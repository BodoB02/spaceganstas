using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorLordDefense : MonoBehaviour
{
    public GameObject meteorPrefab;
    public int numberOfDefenseMeteors = 5;
    public float rotationRadius = 3f;
    public float rotationSpeed = 20f;
    public float respawnDelay = 15f; // Időzítő, amely meghatározza, hogy mennyi idő után spawnoljanak újra a meteoritok

    private List<GameObject> defenseMeteors;
    private Collider2D meteorLordCollider;
    private DamageHandler damageHandler; // Hivatkozás a DamageHandler komponensre
    private bool isPulsating = false;

    void Start()
    {
        damageHandler = GetComponent<DamageHandler>(); // Hivatkozás megszerzése a DamageHandler komponensre
        meteorLordCollider = GetComponent<Collider2D>();
        SpawnDefenseMeteors();
    }

    void Update()
    {
        RotateDefenseMeteors();
        UpdateBossStrength();
        UpdateBossStrength();
    }

    void SpawnDefenseMeteors()
    {
        ClearDefenseMeteors(); // Korábbi meteoritok törlése

        defenseMeteors = new List<GameObject>();
        for (int i = 0; i < numberOfDefenseMeteors; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfDefenseMeteors;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * rotationRadius;
            Vector3 spawnPosition = transform.position + offset;
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
            meteor.transform.parent = transform;

            // Ignore collision between meteors and MeteorLord
            Collider2D meteorCollider = meteor.GetComponent<Collider2D>();
            if (meteorCollider != null && meteorLordCollider != null)
            {
                Physics2D.IgnoreCollision(meteorCollider, meteorLordCollider);
            }

            defenseMeteors.Add(meteor);
        }
    }

    void ClearDefenseMeteors()
    {
        if (defenseMeteors == null) return;

        foreach (GameObject meteor in defenseMeteors)
        {
            if (meteor != null)
            {
                Destroy(meteor);
            }
        }
        defenseMeteors.Clear();
    }

    void RotateDefenseMeteors()
    {
        for (int i = 0; i < defenseMeteors.Count; i++)
        {
            if (defenseMeteors[i] != null)
            {
                defenseMeteors[i].transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void UpdateBossStrength()
    {
        if (damageHandler == null) return;

        float currentHealthRatio = (float)damageHandler.health / damageHandler.GetMaxHealth();

        if (currentHealthRatio <= 0.35f)
        {
            StartCoroutine(HandlePhaseChange(100f, 5f, 10));
            if (!isPulsating)
            {
                StartCoroutine(PulsateRadius());
            }
        }
        else if (currentHealthRatio <= 0.6f)
        {
            StartCoroutine(HandlePhaseChange(50f, 10f, 7));
        }
        else
        {
            StartCoroutine(HandlePhaseChange(20f, 15f, 5));
        }
    }

    IEnumerator HandlePhaseChange(float newRotationSpeed, float newRespawnDelay, int newNumberOfDefenseMeteors)
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        rotationSpeed = newRotationSpeed;
        respawnDelay = newRespawnDelay;
        if (numberOfDefenseMeteors != newNumberOfDefenseMeteors)
        {
            numberOfDefenseMeteors = newNumberOfDefenseMeteors;
            SpawnDefenseMeteors();
        }

    }

    IEnumerator PulsateRadius()
    {
        isPulsating = true;
        while (damageHandler.health <= damageHandler.GetMaxHealth() * 0.35f)
        {
            float originalRadius = rotationRadius;
            float targetRadius = 8f;
            float pulseTime = 2f;

            // Növekedés
            float elapsed = 0f;
            while (elapsed < pulseTime)
            {
                rotationRadius = Mathf.Lerp(originalRadius, targetRadius, elapsed / pulseTime);
                elapsed += Time.deltaTime;
                UpdateMeteorPositions();
                yield return null;
            }
            rotationRadius = targetRadius;

            // Csökkenés
            elapsed = 0f;
            while (elapsed < pulseTime)
            {
                rotationRadius = Mathf.Lerp(targetRadius, originalRadius, elapsed / pulseTime);
                elapsed += Time.deltaTime;
                UpdateMeteorPositions();
                yield return null;
            }
            rotationRadius = originalRadius;

            yield return new WaitForSeconds(1f); // Várakozás a következő pulzálás előtt
        }
        isPulsating = false;
    }

    void UpdateMeteorPositions()
    {
        for (int i = 0; i < defenseMeteors.Count; i++)
        {
            if (defenseMeteors[i] != null)
            {
                float angle = i * Mathf.PI * 2 / numberOfDefenseMeteors;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * rotationRadius;
                defenseMeteors[i].transform.position = transform.position + offset;
            }
        }
    }
}