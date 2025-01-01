using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreSceneScript : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        /*if (!PlayerPrefs.HasKey("IsFirstLaunch"))
        {
            PlayerPrefs.SetInt("IsFirstLaunch", 1); // Az első indítást mentjük
            PlayerPrefs.Save();

            // High score reset
            OnResetHighScoreButton();
        }*/
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        int minutes = Mathf.FloorToInt(highScore / 60F);
        int seconds = Mathf.FloorToInt(highScore % 60F);
        highScoreText.text = string.Format("High Score: {0:00}:{1:00}", minutes, seconds);
    }

    // Vissza a főmenübe gomb kezelése
    public void OnBackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); // Visszatérés a főmenübe
    }

    // High Score reset gomb kezelése
    public void OnResetHighScoreButton()
    {
        PlayerPrefs.DeleteKey("HighScore"); // High score törlése nullára állítva
        PlayerPrefs.Save(); // High score törlése
        highScoreText.text = "High Score: --:--"; // Megjelenítés frissítése
    }
}
