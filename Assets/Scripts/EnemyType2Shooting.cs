using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingType2 : MonoBehaviour
{
    public Vector3 Offset = new Vector3(0, 0.5f, 0);
    public GameObject bulletPrefab;
    public float fireDelay = 0.5f;
    private Camera mainCamera; // Kamera, amely automatikusan beállításra kerül
    public float shootingDelay = 1f;
    private float cooldownTimer = 0;
    private int BulletLayer;
    private EnemyFacesPlayer facesPlayer;

    void Start()
    {
        BulletLayer = gameObject.layer;
        facesPlayer = GetComponent<EnemyFacesPlayer>();

        // Keressük meg a "MainCamera" címkével rendelkező kamerát és állítsuk be mainCamera-nak
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera found with tag 'MainCamera'. Make sure your main camera is tagged appropriately.");
        }
        cooldownTimer = shootingDelay;
    }

    void Update()
    {
        // Ellenőrzés, hogy az ellenség néz-e a játékos irányába, és hogy a kamera látóterében van-e
        if (facesPlayer == null || !facesPlayer.IsFacingPlayer() || !IsInView())
        {
            return;
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = fireDelay;
            Vector3 offset = transform.rotation * Offset;

            GameObject bulletGo = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            bulletGo.layer = BulletLayer;
        }
    }

    // Ellenőrzés, hogy az ellenség a kamera látóterében van-e
    private bool IsInView()
    {
        if (mainCamera == null)
        {
            return false;
        }

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1 && viewportPoint.z > 0;
    }
}
