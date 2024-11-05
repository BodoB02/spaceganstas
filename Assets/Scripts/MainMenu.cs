using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // A játék jelenetének betöltése
        SceneManager.LoadScene("Level1"); // Cseréld le a "GameScene"-t a játékod jelenetének nevére
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
