using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public void StartGame()
    {
        ShipManager.Instance.ResetToBaseVersion();
        // A játék jelenetének betöltése
        SceneManager.LoadScene("Level1"); // Cseréld le a "GameScene"-t a játékod jelenetének nevére
    }
    public void OnViewHighScoreButton()
    {
        SceneManager.LoadScene("OptionsScene"); // Betöltjük a High Score jelenetet, ahol az eddigi legjobb eredmények megtekinthetők
    }
    public void ExitGame()
    {
        // Kilépés a játékból
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Csak editor módban
#endif
    }
}
