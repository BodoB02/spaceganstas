using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sebzés kezelését végző osztály
public class ItemsDamageHandler : MonoBehaviour
{
    // Az objektum életereje
    private int health = 1;

    // A maximális életerő tárolása
    private int maxHealth;

    // A Start metódus meghívásakor történik meg a szükséges inicializálás
    void Start()
    {
        // A maximális életerő beállítása a kezdeti életerő értékére
        maxHealth = health;
    }
    // Amikor az objektum ütközik valamivel, ez a metódus hívódik meg
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ha az objektum egy "HealthPickup" nevű tárggyal ütközik, növeljük az életerőt
            health--;
            Destroy(gameObject);
        }
    }
    // Az Update metódus minden frame-ben meghívódik
    void Update()
    {
        // Ha az életerő elfogyott, meghívjuk a Die metódust
        if (health <= 0)
        {
            Die();
        }
    }
    // A karakter elpusztítását végző metódus
    void Die()
    {
        // Megsemmisítjük az objektumot
        Destroy(gameObject);
    }
}

