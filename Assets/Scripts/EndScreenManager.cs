using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;  // A gördülő szöveg megjelenítője
    public TextMeshProUGUI promptText; // A "Press ESC to return to main menu" szöveg
    public float scrollSpeed = 20f;   // A gördülés sebessége
    private float startPositionY;     // Kezdőpozíció Y értéke
    private float endPositionY;       // Végpozíció Y értéke


    void Start()
    {
        // Szöveg méretének meghatározása
        RectTransform textRect = storyText.rectTransform;

        // Kezdőpozíció a képernyő alján kívül
        startPositionY = -Screen.height / 2 - textRect.rect.height;

        // Végpozíció a képernyő tetején kívül
        endPositionY = Screen.height / 2;

        // Szöveg pozíciójának beállítása
        textRect.anchoredPosition = new Vector2(0, startPositionY);

        // Prompt szöveg elrejtése kezdetben
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Gördítsük a szöveget felfelé
        storyText.rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        // Ellenőrizzük, hogy a szöveg teljesen legördült-e
        if (storyText.rectTransform.anchoredPosition.y >= endPositionY)
        {
            // Prompt szöveg megjelenítése
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
            }
        }

        // Ha az ESC gomb lenyomva, betöltjük a főmenüt
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Betöltjük a főmenüt
    }
}
