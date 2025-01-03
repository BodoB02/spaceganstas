using UnityEngine;
using System.Collections;

public class MeteorLordMovement : MonoBehaviour
{
    public float speed = 0.5f; // A Meteor Lord sebessége (lassabb követés)
    private Transform player; // A játékos objektum referenciája

    private float spawnAreaMinX;
    private float spawnAreaMaxX;
    private float spawnAreaMinY;
    private float spawnAreaMaxY;
    private Vector2 movementDirection;
    private DamageHandler damageHandler; // Hivatkozás a DamageHandler komponensre
    private int originalLayer; // Az eredeti layer eltárolása
    private bool isShaking = false; // Rázkódási állapot jelzése
    private bool hasShakenAt100 = false; // Jelzi, hogy a rázkódás már megtörtént 100%-nál
    private bool hasShakenAt60 = false; // Jelzi, hogy a rázkódás már megtörtént 60%-nál
    private bool hasShakenAt35 = false; // Jelzi, hogy a rázkódás már megtörtént 35%-nál
    [Header("Audio Settings")]
    public AudioClip damageSound; // Egyetlen hangfájl minden életerőszinthez
    private AudioSource audioSource;
    private bool isAudioPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        damageHandler = GetComponent<DamageHandler>(); // Hivatkozás megszerzése a DamageHandler komponensre
        originalLayer = gameObject.layer; // Eredeti layer eltárolása

        // Játékos keresése tag alapján
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            PlayerMovment movementScript = player.GetComponent<PlayerMovment>();
            if (movementScript != null)
            {
                spawnAreaMinX = -movementScript.mapSize.x / 2;
                spawnAreaMaxX = movementScript.mapSize.x / 2;
                spawnAreaMinY = -movementScript.mapSize.y / 2;
                spawnAreaMaxY = movementScript.mapSize.y / 2;
            }
            else
            {
                Debug.LogError("Movment script is missing from the player object.");
                return;
            }
        }
        else
        {
            Debug.LogError("Player object is not assigned or not found.");
            return;
        }
    }

    void Update()
    {
        if (!isShaking)
        {
            FollowPlayer();
            ClampPositionWithinBounds();
        }
        else if (!isAudioPlaying && isShaking)
        {
            audioSource.PlayOneShot(damageSound, 0.2f);
            StartCoroutine(ResetAudioState());
        }
        UpdateBossStrength();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * speed * Time.deltaTime);
        }
    }

    void ClampPositionWithinBounds()
    {
        // Mozgás korlátozása a pálya határain belül
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, spawnAreaMinX, spawnAreaMaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, spawnAreaMinY, spawnAreaMaxY);
        transform.position = newPosition;
    }

    void UpdateBossStrength()
    {
        if (damageHandler == null) return;

        float currentHealthRatio = (float)damageHandler.health / damageHandler.GetMaxHealth();

        if (currentHealthRatio == 1f && !hasShakenAt100)
        {
            StartCoroutine(ShakeAndBecomeInvulnerable());
            hasShakenAt100 = true;
        }
        else if (currentHealthRatio <= 0.6f && !hasShakenAt60)
        {
            StartCoroutine(ShakeAndBecomeInvulnerable());
            hasShakenAt60 = true;
        }
        else if (currentHealthRatio <= 0.35f && !hasShakenAt35)
        {
            StartCoroutine(ShakeAndBecomeInvulnerable());
            hasShakenAt35 = true;
        }

        if (currentHealthRatio <= 0.35f && !isShaking)
        {
            speed = 2f; // Gyorsabb sebesség, amikor a boss életereje nagyon alacsony
        }
        else if (currentHealthRatio <= 0.6f && !isShaking)
        {
            speed = 1.5f;
        }
        else if (!isShaking)
        {
            speed = 1f; // Lassabb sebesség alapértelmezett esetben
        }
    }

    IEnumerator ShakeAndBecomeInvulnerable()
    {
        isShaking = true;
        float shakeDuration = 2f;
        float shakeMagnitude = 0.1f;
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            gameObject.layer = 11;
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // Visszaállítás az eredeti pozícióra
        gameObject.layer = originalLayer; // Visszaállítás az eredeti rétegre
        isShaking = false;
    }
    IEnumerator ResetAudioState()
    {
        isAudioPlaying = true;
        yield return new WaitForSeconds(damageSound.length); // Megvárja a hang lejátszását
        isAudioPlaying = false;
    }
}
