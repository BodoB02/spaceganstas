using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PauseMenuHandlerTests
{
    [UnityTest]
    public IEnumerator PauseAndResumeGame_UpdatesGameStateCorrectly()
    {
        // Előkészítés: Hozzuk létre a GameManager-t és a PauseMenuHandler-t
        var gameManager = new GameObject("GameManager");
        var pauseMenuHandler = gameManager.AddComponent<PauseMenuHandler>();
        pauseMenuHandler.pauseMenu = new GameObject("PauseMenu");
        var fadeOverlay = new GameObject("FadeOverlay");
        var canvasGroup = fadeOverlay.AddComponent<CanvasGroup>();
        pauseMenuHandler.fadeOverlay = canvasGroup;

        // Reflektálással elérjük a privát PauseGame és ResumeGame metódusokat
        var pauseGameMethod = typeof(PauseMenuHandler).GetMethod("PauseGame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var resumeGameMethod = typeof(PauseMenuHandler).GetMethod("ResumeGame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Cselekvés - A játék szüneteltetése
        pauseGameMethod.Invoke(pauseMenuHandler, null);
        for (float elapsed = 0; elapsed < 2f; elapsed += Time.unscaledDeltaTime)
        {
            pauseMenuHandler.fadeOverlay.alpha = Mathf.Lerp(0, 1, elapsed / 0.5f);
            yield return null;
        }

        // Ellenőrzés - A játéknak szünetelnie kell
        Assert.AreEqual(0f, Time.timeScale, "A játéknak szünetelnie kell.");
        Assert.IsTrue(pauseMenuHandler.pauseMenu.activeSelf, "A szünet menünek aktívnak kell lennie.");
        Assert.GreaterOrEqual(canvasGroup.alpha, 0.9f, "A fade overlay-nek majdnem teljesen átlátszatlannak kell lennie a fade-in után.");

        // Cselekvés - A játék folytatása
        resumeGameMethod.Invoke(pauseMenuHandler, null);
        for (float elapsed = 0; elapsed < 2f; elapsed += Time.unscaledDeltaTime)
        {
            pauseMenuHandler.fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsed / 0.5f);
            yield return null;
        }

        // Ellenőrzés - A játéknak folytatódnia kell
        Assert.AreEqual(1f, Time.timeScale, "A játéknak újra futnia kell.");
        Assert.IsFalse(pauseMenuHandler.pauseMenu.activeSelf, "A szünet menünek inaktívnak kell lennie.");
        Assert.LessOrEqual(canvasGroup.alpha, 0.1f, "A fade overlay-nek majdnem teljesen átlátszónak kell lennie a fade-out után.");
    }
}
