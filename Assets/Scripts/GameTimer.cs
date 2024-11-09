using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Játékidőt nyomon követő osztály
public class GamerTimer : MonoBehaviour
{
    // A UI Text elem, amely megjeleníti az eltelt időt a felhasználónak
    public TextMeshProUGUI timerText;

    // Az eltelt idő mérése
    private float timeElapsed;
    private float highScore;

    // Játék futásának állapota (fut-e a számláló)
    private bool isGameRunning;
    private bool playerDied = false; // Játékos állapotának nyomon követése
    private Transform player;

    // A script kezdeti inicializálása, amikor az objektum először létrejön
    void Awake()
    {
        // Ha az aktuális jelenet az "EndScene" vagy a "MainMenuScene", akkor megsemmisítjük az időmérő objektumot
        if (SceneManager.GetActiveScene().name == "EndScene" || SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject); // Ezzel leállítod a számlást, amikor az EndScene vagy MainMenu jelenet van betöltve
        }
        else
        {
            // A script és az objektum megmarad akkor is, ha egy új jelenet töltődik be
            DontDestroyOnLoad(gameObject);
        }
    }

    // Az időmérő indulásakor hívódik meg
    void Start()
    {
        // Kezdeti értékek beállítása
        timeElapsed = 0f;
        highScore = PlayerPrefs.GetFloat("HighScore", Mathf.Infinity); // Az eddigi legjobb eredmény betöltése
        isGameRunning = true; // A számláló futni kezd
    }

    // Minden frame-ben meghívódó metódus, amely frissíti az eltelt időt
    void Update()
    {
        if (!(SceneManager.GetActiveScene().name == "EndScene" || SceneManager.GetActiveScene().name == "MainMenu"))
        {
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

            // Ha nincs játékos, akkor a playerDied érték true lesz
            if (player == null)
            {
                playerDied = true;
            }
        }

        // Ha az aktuális jelenet az "EndScene", akkor mentjük a high score-t és megsemmisítjük az időmérő objektumot
        if (SceneManager.GetActiveScene().name == "EndScene" || SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "HighScoreMenu")
        {
            Destroy(gameObject); // Az időmérő elpusztítása az EndScene jelenetben
            if (SceneManager.GetActiveScene().name == "EndScene")
            {
                SaveHighScore();
            }
        }

        // Ha a játék fut, növeljük az eltelt időt
        if (isGameRunning)
        {
            // Az eltelt idő növelése az idő múlásával
            timeElapsed += Time.deltaTime;

            // Az eltelt idő átváltása percekre és másodpercekre
            int minutes = Mathf.FloorToInt(timeElapsed / 60F);
            int seconds = Mathf.FloorToInt(timeElapsed % 60F);

            // A felhasználói felületen megjelenítjük az eltelt időt, formázva (00:00)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Az időmérő leállítása
    public void StopTimer()
    {
        isGameRunning = false; // Leállítjuk az időmérőt
    }

    // Az időmérő újraindítása
    public void StartTimer()
    {
        isGameRunning = true; // Az időmérő újraindul
        timeElapsed = 0f; // Az eltelt időt nullára állítjuk
    }

    // High Score mentése
    private void SaveHighScore()
    {
        if ((timeElapsed < highScore || highScore <= 1f) && !playerDied)
        {
            highScore = timeElapsed;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save(); // High score mentése
        }
    }
}
