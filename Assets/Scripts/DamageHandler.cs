using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sebzés kezelését végző osztály
public class DamageHandler : MonoBehaviour
{
    // Az időtartam, ameddig a karakter sebezhetetlen (invulnerable) egy találat után
    public float invulnPeriod = 0;

    // Az objektum életereje
    public int health = 2;

    // Sebezhetetlenségi idő számlálója
    float invulnTimer = 0;

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

        // Hivatkozás megszerzése a MeteorlordAttack komponensre
    }

    // Amikor az objektum ütközik valamivel, ez a metódus hívódik meg
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // Ha az objektum egy "HealthPickup" nevű tárggyal ütközik, növeljük az életerőt
            health++;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            // Frissítjük a HP csíkot
            UpdateHealthBar();
            // Megsemmisítjük a felvett tárgyat
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item")) { }
        else
        {
            // Csökkentjük az életerőt
            health--;

            // Beállítjuk a sebezhetetlenségi időszakot
            invulnTimer = invulnPeriod;

            // Az objektumot sebezhetetlenségi rétegre helyezzük, hogy ne kapjon további találatokat
            gameObject.layer = 11;

            // Frissítjük a HP csíkot
            UpdateHealthBar();
        }
    }

    // Az Update metódus minden frame-ben meghívódik
    void Update()
    {
        // Csökkentjük a sebezhetetlenségi időszakot
        invulnTimer -= Time.deltaTime;

        // Ha a sebezhetetlenségi idő lejárt, visszaállítjuk az eredeti réteget
        if (invulnTimer <= 0)
        {
            gameObject.layer = correctLayer;
        }

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
    public int GetMaxHealth() { return maxHealth; }
}
