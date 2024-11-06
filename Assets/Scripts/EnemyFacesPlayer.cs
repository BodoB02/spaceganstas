using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// A Player felé fordulást végző osztály
public class EnemyFacesPlayer : MonoBehaviour
{
    // A játékos Transform objektuma, amelyet követünk
    Transform player;

    // Forgási sebesség fokokban (degree per second)
    public float rotSpeed = 90f;

    // Minden frame-ben meghívódó metódus, amely kezeli a forgást
    void Update()
    {
        // Ellenőrizzük, hogy a játékos referencia be van-e állítva
        if (player == null)
        {
            // Megkeressük a "Player" nevű objektumot a jelenetben
            GameObject go = GameObject.Find("Player");

            // Ha megtaláltuk a játékost, eltároljuk a Transform referencia
            if (go != null)
            {
                player = go.transform;
            }
        }

        // Ha nincs játékos, akkor nincs további teendő
        if (player == null) { return; }

        // A játékos felé mutató irány kiszámítása
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        // A célzott forgási szög kiszámítása (az Y és X tengelyek alapján)
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        // A kívánt forgás beállítása a z tengely körül
        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        // Forgatás a jelenlegi irányból a kívánt irányba a megadott forgási sebességgel
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
    }

    // Ellenőrzi, hogy a játékos felé nézünk-e (visszatér true-val, ha igen)
    public bool IsFacingPlayer()
    {
        // Ha nincs játékos, visszatér false értékkel
        if (player == null) return false;

        // A játékos felé mutató irány kiszámítása
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        // A célzott forgási szög kiszámítása
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        // A jelenlegi forgás és a kívánt forgás közötti szög különbség
        float angleDifference = Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, zAngle));

        // Visszaadja, hogy a szög különbség kisebb-e, mint 5 fok (ami pontos irányt jelent)
        return angleDifference < 5f;
    }
}
