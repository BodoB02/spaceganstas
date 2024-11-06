using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;          // A gördülő szöveg megjelenítője
    public float scrollSpeed = 20f; // A gördülés sebessége
    private float startPositionY;   // Kezdőpozíció Y értéke
    private float endPositionY;     // Végpozíció Y értéke

    void Start()
    {
        // A kezdőpozíció a képernyő alján kicsit alul helyezkedik el
        startPositionY = -Screen.height / 2;
        endPositionY = storyText.rectTransform.sizeDelta.y + Screen.height / 2;

        // Állítsuk be a szöveg kezdő pozícióját
        storyText.rectTransform.anchoredPosition = new Vector2(0, startPositionY);
    }

    void Update()
    {
        // Gördítsük a szöveget felfelé
        storyText.rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Ha a szöveg teljesen legördült, betöltjük a főmenüt
        if (storyText.rectTransform.anchoredPosition.y >= endPositionY)
        {
            LoadMainMenu();
        }
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Betöltjük a főmenüt
    }
}