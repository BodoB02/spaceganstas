using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;         // A UI Text objektum, amely kiírja a szint nevét
    public TextMeshProUGUI gameOverText;      // A UI Text objektum, amely kiírja a "Game Over" feliratot
    public string levelName;                  // A szint neve, pl. "First Level"
    public GameObject player;                 // A player objektum
    public float displayTime = 2f;            // A szint név megjelenítésének ideje
    public float gameOverDelay = 3f;          // Idő a "Game Over" után, mielőtt visszalépünk a menübe
    public Spawner spawner;                   // A Spawner referencia

    void Start()
    {
        StartCoroutine(DisplayLevelName());
        // Az aktuális szint neve megjelenik a játék indulásakor
    }

    void Update()
    {
        if (player == null) // Ha a player meghalt
        {
            StartCoroutine(HandlePlayerDeath());
            return;
        }

        // Csak akkor engedjük meg a szint teljesítését, ha az ellenségek már létrejöttek

        if (SceneManager.GetActiveScene().name == "Level4" && spawner.RemainingEnemies() == 0 && player != null) // Csak akkor lépjen tovább, ha nincs több enemy
        {
            LoadNextLevel();
        }
        else if (SceneManager.GetActiveScene().name != "Level4" && spawner.RemainingItems() == 0 && player != null) // Más szinteken, ha nincs több item
        {
            LoadNextLevel();
        }
    }


    IEnumerator DisplayLevelName()
    {
        Time.timeScale = 0; // Pausoljuk a játékot
        if (levelText != null)
        {
            levelText.text = levelName;
            levelText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(displayTime); // Valós idejű várakozás a paus mellett
            levelText.gameObject.SetActive(false);
        }
        Time.timeScale = 1; // Folytatjuk a játékot
    }

    IEnumerator HandlePlayerDeath()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";
        }
        yield return new WaitForSeconds(gameOverDelay); // Várakozás a Game Over után
        SceneManager.LoadScene("MainMenu"); // Vissza a főmenübe
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(3f); // 3 másodperc késleltetés a Spawner beállítása érdekében
        StartCoroutine(DisplayLevelName());
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            LoadEndScene();
        }
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
