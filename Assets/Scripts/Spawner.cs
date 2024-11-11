using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;           // Az ellenség prefabok listája
    public GameObject[] meteorPrefabs;          // A meteorfajták prefabjainak listája
    public GameObject[] powerupPrefabs;         // Powerup prefabok listája
    public GameObject[] itemPrefabs;            // Itemek listája
    public int[] meteorCounts;                  // Meteorok száma típustól függően
    public int[] enemyCounts;                   // Ellenségek száma típustól függően
    public int[] itemCount;                     // Itemek száma típustól függően
    public int powerupCount;                    // Powerupok száma
    public GameObject mapBoundaryObject;        // A határ objektum, amely meghatározza a pálya méretét

    private float spawnAreaMinX;
    private float spawnAreaMaxX;
    private float spawnAreaMinY;
    private float spawnAreaMaxY;

    void Start()
    {
        // A határ objektum koordinátáinak meghatározása
        if (mapBoundaryObject != null)
        {
            PlayerMovment movementScript = mapBoundaryObject.GetComponent<PlayerMovment>();
            if (movementScript != null)
            {
                spawnAreaMinX = -movementScript.mapSize.x / 2;
                spawnAreaMaxX = movementScript.mapSize.x / 2;
                spawnAreaMinY = -movementScript.mapSize.y / 2;
                spawnAreaMaxY = movementScript.mapSize.y / 2;
            }
            else
            {
                return;
            }
        }

        SpawnEnemiesAndMeteorsAndItems(); // Ellenségek és meteorok és itemek generálása a játék indulásakor
        SpawnPowerups(); // Powerupok generálása a játék indulásakor
    }
    void SpawnEnemiesAndMeteorsAndItems()
    {
        // Ellenségek generálása
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < enemyCounts[i]; j++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                int attempts = 0;
                while (IsPositionOccupied(spawnPosition) && attempts < 10) // Maximum 10 próbálkozás
                {
                    spawnPosition = GetRandomSpawnPosition();
                    attempts++;
                }
                if (attempts < 10) // Csak akkor hozzuk létre, ha sikerült üres helyet találni
                {
                    Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
                }
            }
        }
        //Itemek generálása
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            for (int j = 0; j < itemCount[i]; j++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                int attempts = 0;
                while (IsPositionOccupied(spawnPosition) && attempts < 10) // Maximum 10 próbálkozás
                {
                    spawnPosition = GetRandomSpawnPosition();
                    attempts++;
                }
                if (attempts < 10) // Csak akkor hozzuk létre, ha sikerült üres helyet találni
                {
                    Instantiate(itemPrefabs[i], spawnPosition, Quaternion.identity);
                }
            }
        }
        // Meteorok generálása
        for (int i = 0; i < meteorPrefabs.Length; i++)
        {
            for (int j = 0; j < meteorCounts[i]; j++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                int attempts = 0;
                while (IsPositionOccupied(spawnPosition) && attempts < 10) // Maximum 10 próbálkozás
                {
                    spawnPosition = GetRandomSpawnPosition();
                    attempts++;
                }
                if (attempts < 10) // Csak akkor hozzuk létre, ha sikerült üres helyet találni
                {
                    Instantiate(meteorPrefabs[i], spawnPosition, Quaternion.identity);
                }
            }
        }
    }
    void SpawnPowerups()
    {
        // Powerupok generálása
        for (int i = 0; i < powerupCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            int attempts = 0;
            while (IsPositionOccupied(spawnPosition) && attempts < 10) // Maximum 10 próbálkozás
            {
                spawnPosition = GetRandomSpawnPosition();
                attempts++;
            }
            if (attempts < 10) // Csak akkor hozzuk létre, ha sikerült üres helyet találni
            {
                int randomIndex = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnAreaMinX + 3f, spawnAreaMaxX - 3f); // Biztosítjuk, hogy ne a széleken spawnoljon
        float y = Random.Range(spawnAreaMinY + 3f, spawnAreaMaxY - 3f);
        Vector3 spawnPosition = new Vector3(x, y, 0);
        return spawnPosition;
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1.5f); // Megnöveltük a sugár méretét 1.5-re a megfelelő távolság érdekében
        return colliders.Length > 0;
    }
}
