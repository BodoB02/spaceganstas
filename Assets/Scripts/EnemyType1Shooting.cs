using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingType1 : MonoBehaviour
{
    public Vector3 Offset = new Vector3(0, 0.5f, 0);
    public GameObject bulletPrefab;
    public float fireDelay = 0.5f;
    float cooldownTimer = 0;
    int BulletLayer;
    EnemyFacesPlayer facesPlayer; // Referencia a FacesPlayer szkriptre

    void Start()
    {
        BulletLayer = gameObject.layer;
        facesPlayer = GetComponent<EnemyFacesPlayer>(); // Hivatkozás megszerzése
    }

    void Update()
    {
        if (facesPlayer == null || !facesPlayer.IsFacingPlayer()) return; // Csak akkor lőjön, ha a player felé néz

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = fireDelay;
            Vector3 offset = transform.rotation * Offset;

            GameObject bulletGo = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            bulletGo.layer = BulletLayer;
        }
    }
}