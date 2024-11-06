using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ellenség mozgását vezérlő osztály
public class EnemyMoveForward : MonoBehaviour
{
    // Az ellenség maximális mozgási sebessége
    public float maxSpeed = 5f;

    // Minden frame-ben meghívódó metódus, amely felelős az ellenség mozgatásáért
    void Update()
    {
        // Az aktuális pozíció lekérése
        Vector3 pos = transform.position;

        // Sebesség kiszámítása: az ellenség függőlegesen (Y tengely) mozog előre
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        // Új pozíció kiszámítása a forgatás figyelembevételével (előre haladás a forgás irányában)
        pos += transform.rotation * velocity;

        // Az ellenség pozíciójának frissítése
        transform.position = pos;
    }
}
