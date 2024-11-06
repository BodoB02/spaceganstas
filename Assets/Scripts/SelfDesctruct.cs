using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Az objektum automatikus megsemmisítését végző osztály
public class SelfDestruct : MonoBehaviour
{
    // Az időzítő, amely meghatározza, hogy hány másodperc múlva semmisítse meg magát az objektum
    public float timer = 2f;

    // Minden frame-ben meghívódó metódus
    void Update()
    {
        // Csökkentjük az időzítőt az eltelt idővel
        timer -= Time.deltaTime;

        // Ha az időzítő lejár (0 vagy kevesebb), megsemmisítjük az objektumot
        if (timer <= 0)
        {
            Destroy(gameObject); // Az objektum megsemmisítése
        }
    }
}
