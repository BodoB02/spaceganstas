using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpin : MonoBehaviour
{
    public float rotationSpeed = 50f; // A forgás sebessége
    public bool clockwise = true; // A forgás iránya: igaz (true) = óramutató járásával megegyező, hamis (false) = ellentétes

    void Update()
    {
        // Állítsuk be a forgási irányt a `clockwise` értéke alapján
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0, 0, direction * rotationSpeed * Time.deltaTime);
    }
}