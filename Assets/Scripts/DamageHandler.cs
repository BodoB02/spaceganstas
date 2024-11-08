using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sebzés kezelését végző osztály
public class DamageHandler : MonoBehaviour
{
    // Az objektum életereje
    public int health = 2;

    // A maximális életerő tárolása
    private int maxHealth;

    // Az eredeti réteg, amin az objektum található
    int correctLayer;

    // A HP csíkhoz tartozó UI Image komponens
    public Image healthBar;

    // A Start metódus meghívásakor történik meg a szükséges inicializálás
    void Start()
    {
        // Eltároljuk az eredeti réteget, amelyen az objektum található
        correctLayer = gameObject.layer;

        // A maximális életerő beállítása a kezdeti életerő értékére
        maxHealth = health;

        // A HP csík frissítése
        UpdateHealthBar();
    }

    // Amikor az objektum ütközik valamivel, ez a metódus hívódik meg
    void OnTriggerEnter2D()
    {
        // Csökkentjük az életerőt
        health--;

        // Frissítjük a HP csíkot
        UpdateHealthBar();
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

    // A HP csík frissítése a jelenlegi életerő alapján
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // A HP százalékos értékének kiszámítása
            float healthPercent = (float)health / maxHealth;

            // A HP csík szélességének módosítása a százalék alapján
            healthBar.fillAmount = healthPercent;
        }
    }
}
