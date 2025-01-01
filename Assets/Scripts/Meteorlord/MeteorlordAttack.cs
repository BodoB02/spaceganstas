using System.Collections;
using UnityEngine;

public class MeteorlordAttack : MonoBehaviour
{
    public float initialDelay = 3f; // Késleltetés ideje a támadások előtt
    [Header("Dash Settings")]
    public float dashSpeed = 5f;
    public float dashCooldown = 10f;
    private bool isDashing = false;
    [Header("Shower Settings")]
    public GameObject meteorPrefab;
    public float meteorForce = 5f; // A meteor kilökési ereje
    public int numberOfMeteors = 4; // A kilőtt meteorok száma
    public float meteorAngleSpread = 85f; // Szög, amelyen belül kilőjük a meteorokat
    private bool initialDelayPassed = false; // Jelzi, hogy a kezdeti késleltetés lejárt-e
    private bool isMeteorShowerActive = false; //Jelzi hogy aktív a meteorshower funkció

    private DamageHandler damageHandler; // Hivatkozás a DamageHandler komponensre

    void Start()
    {
        damageHandler = GetComponent<DamageHandler>(); // Hivatkozás megszerzése a DamageHandler komponensre
        StartCoroutine(WaitBeforeAction()); // Késleltetés a boss számára a spawnolás után
    }

    IEnumerator WaitBeforeAction()
    {
        yield return new WaitForSeconds(initialDelay); // Késleltetés a boss számára a spawnolás után
        initialDelayPassed = true;
        InvokeRepeating(nameof(MeteorShower), 0f, dashCooldown); // Támadások ismétlődése 8 másodpercenként
    }

    void Update()
    {
        if (!initialDelayPassed || isDashing) return; // Ha a kezdeti késleltetés még nem járt le vagy a boss éppen várakozik, ne csináljon semmit

        if (GameObject.FindGameObjectWithTag("Player") != null && !isDashing)
        {
            StartCoroutine(DashTowardsPlayer());
        }

        UpdateBossStrength(); // Frissítjük a boss erősségét az életerő függvényében
    }

    void MeteorShower()
    {
        if (isMeteorShowerActive) return; // Elkerüli a párhuzamos futást
        isMeteorShowerActive = true;

        for (int i = 0; i < numberOfMeteors; i++)
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 spawnPosition = transform.position + transform.rotation * offset;
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, transform.rotation);
            Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Physics2D.IgnoreCollision(meteor.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                float angle = -meteorAngleSpread * (numberOfMeteors - 1) / 2 + i * meteorAngleSpread;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                rb.velocity = direction * meteorForce;
            }

            Destroy(meteor, 5f);
        }

        StartCoroutine(MeteorShowerRoutine());
    }

    IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        Vector2 dashDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        float dashTime = 1f; // Az idő, ameddig a dash tart
        float timer = 0f;

        while (timer < dashTime)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

    void UpdateBossStrength()
    {
        if (damageHandler == null) return;

        float currentHealthRatio = (float)damageHandler.health / damageHandler.GetMaxHealth();

        if (currentHealthRatio <= 0.35f)
        {
            dashSpeed = 5f;
            dashCooldown = 5f;
            meteorForce = 8f;
            numberOfMeteors = 6;
            meteorAngleSpread = 60f;
        }
        else if (currentHealthRatio <= 0.6f)
        {
            dashSpeed = 5f;
            dashCooldown = 6f;
            meteorForce = 7f;
            numberOfMeteors = 5;
            meteorAngleSpread = 72f;
        }
        else
        {
            dashSpeed = 4f;
            dashCooldown = 7f;
            meteorForce = 4f;
            numberOfMeteors = 3;
            meteorAngleSpread = 120f;
        }
    }
    IEnumerator MeteorShowerRoutine()
    {
        for (int i = 0; i < numberOfMeteors; i++)
        {
            yield return new WaitForSeconds(0.2f); // Késleltetés a meteorok között
        }
        isMeteorShowerActive = false; // Itt állítjuk vissza false-ra
    }
}