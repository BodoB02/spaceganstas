using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A lövedékek előre mozgását vezérlő osztály
public class BullettMoveForward : MonoBehaviour
{
    // A lövedék maximális sebessége
    public float maxSpeed = 5f;

    // Update metódus, amely minden frame-ben meghívódik
    void Update()
    {
        // Jelenlegi pozíció lekérése
        Vector3 pos = transform.position;

        // Sebesség kiszámítása: a lövedék függőlegesen (Y tengely) mozog előre
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        // Új pozíció kiszámítása a forgatás figyelembevételével (előre haladás a forgás irányában)
        pos += transform.rotation * velocity;

        // A lövedék pozíciójának frissítése
        transform.position = pos;
    }
}
