using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// A szünet menüt kezelő osztály
public class PauseMenuHandler : MonoBehaviour
{
    // A fade effekt (fekete átlátszó réteg) CanvasGroup referenciája
    public CanvasGroup fadeOverlay; // A CanvasGroup objektumot itt húzd be, amely a fekete fade réteget tartalmazza

    // A szünet menü UI eleme
    public GameObject pauseMenu;    // A szünet menü UI-ját itt húzd be

    // A játék szüneteltetett állapotát jelző változó
    private bool isPaused = false;

    // Minden frame-ben meghívódó metódus
    void Update()
    {
        // Ha megnyomjuk az Escape billentyűt
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ha a játék szüneteltetve van, akkor folytassuk, különben szüneteltessük
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // A játék szüneteltetése
    void PauseGame()
    {
        isPaused = true;

        // Indítjuk a képernyő elsötétítését
        StartCoroutine(FadeIn());

        // A játék megállítása (az idő skálázása 0-ra)
        Time.timeScale = 0f;

        // A szünet menü megjelenítése
        pauseMenu.SetActive(true);
    }

    // A játék folytatása a szüneteltetés után
    void ResumeGame()
    {
        isPaused = false;

        // Indítjuk a képernyő halványítását
        StartCoroutine(FadeOut());

        // A játék idő skálázásának visszaállítása
        Time.timeScale = 1f;

        // A szünet menü elrejtése
        pauseMenu.SetActive(false);
    }

    // Visszalépés a főmenübe
    public void GoToMainMenu()
    {
        // Az idő visszaállítása az alapállapotra
        Time.timeScale = 1f;
        Debug.Log("GoToMainMenu called");
        // Jelenet váltása a főmenüre
        SceneManager.LoadScene("MainMenu");
    }

    // Fade in effekt: a képernyő elsötétítése
    IEnumerator FadeIn()
    {
        // Az elhalványulás időtartama másodpercekben
        float fadeDuration = 0.5f;
        float elapsed = 0f;

        // Amíg az elhalványulás idő nem jár le
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // Az eltelt idő növelése (szüneteltetett állapotban az unscaledDeltaTime használatos)
            fadeOverlay.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration); // Lassan növeljük az átlátszatlanságot
            yield return null; // Várakozás a következő frame-ig
        }

        // Biztosítjuk, hogy az átlátszatlanság teljes legyen
        fadeOverlay.alpha = 1;
    }

    // Fade out effekt: a képernyő halványítása
    IEnumerator FadeOut()
    {
        // Az elhalványulás időtartama másodpercekben
        float fadeDuration = 0.5f;
        float elapsed = 0f;

        // Amíg az elhalványulás idő nem jár le
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // Az eltelt idő növelése (szüneteltetett állapotban az unscaledDeltaTime használatos)
            fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration); // Lassan csökkentjük az átlátszatlanságot
            yield return null; // Várakozás a következő frame-ig
        }

        // Biztosítjuk, hogy az átlátszatlanság nullára csökkenjen
        fadeOverlay.alpha = 0;
    }
}
