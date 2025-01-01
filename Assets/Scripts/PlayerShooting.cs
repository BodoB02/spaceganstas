using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A játékos lövését kezelő osztály
public class PlayerShooting : MonoBehaviour
{
    // A lövés kiindulópontjának eltolása (a játékoshoz képest)
    public Vector3 Offset = new Vector3(0, 0.5f, 0);

    // A lövedék prefabja (amit a játékos lő ki)
    public GameObject bulletPrefab;

    // A lövedék rétege
    int BulletLayer;

    // Lövés közötti késleltetés másodpercben
    public float fireDelay = 0.25f;

    // Lövés közötti idő számlálója
    float cooldownTimer = 0;

    public AudioClip shootSound;
    public float shootSoundVolume = 0.25f;
    // AudioSource komponens referenciája
    private AudioSource audioSource;

    // A script kezdeti inicializálása
    void Start()
    {
        // A lövedék rétegének beállítása a játékos objektum rétegére
        BulletLayer = gameObject.layer;
        // Ellenőrizd, hogy van-e AudioSource komponens, ha nem, add hozzá
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 0.5f;
    }

    // Minden frame-ben meghívódó metódus, amely kezeli a lövést
    void Update()
    {
        // A lövési időzítő csökkentése az eltelt idővel
        cooldownTimer -= Time.deltaTime;

        // Ha az elsődleges lövés gombot (általában bal egérgomb vagy Ctrl) lenyomják és a hűlési idő lejárt
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            // Debug üzenet a konzolba (kommentelve van, de a lövés jelzésére használható)
            //Debug.Log("Pew");

            // Az új lövés hűlési idejének beállítása
            cooldownTimer = fireDelay;

            // Lövés kezdőpontjának kiszámítása a játékos pozíciójához viszonyítva
            Vector3 offset = transform.rotation * Offset;

            // Lövedék példányosítása a megfelelő helyen és irányban
            GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);

            // A lövedék rétegének beállítása a játékos rétegére
            bulletGo.layer = BulletLayer;

            // Lövés hang lejátszása
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
            }
        }
    }
}
