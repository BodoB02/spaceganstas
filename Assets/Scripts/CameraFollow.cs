using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A kamera követését végző osztály
public class CameraFollow : MonoBehaviour
{
    // A cél objektum (pl. játékos), amelyet a kamera követ
    public Transform myTarget;

    // Minden frame-ben meghívódó metódus
    void Update()
    {
        // Ellenőrizzük, hogy a cél objektum nem null-e (létezik)
        if (myTarget != null)
        {
            // A cél objektum pozíciójának lekérése
            Vector3 targPos = myTarget.position;

            // A kamera z pozíciójának megőrzése, hogy a 2D játék esetén a mélység változatlan maradjon
            targPos.z = transform.position.z;

            // A kamera pozíciójának frissítése, hogy a cél pozíciójára kerüljön
            transform.position = targPos;
        }
    }
}
