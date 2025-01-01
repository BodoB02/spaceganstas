using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton példány

    public AudioClip menuMusic; // Főmenü és beállítások zenéje
    public AudioClip levelMusic; // Szintek zenéje
    public AudioClip endSceneMusic; // Befejezés zenéje

    // Hangeffektek
    public AudioClip gameOverSound;
    public AudioClip shipDestroySound;

    private AudioSource musicSource; // Zenei forrás
    private AudioSource sfxSource; // Hangeffekt forrás

    private string lastSceneName = "";

    void Awake()
    {
        // Singleton ellenőrzés
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Zenei forrás inicializálása
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        // Hangeffektek forrásának inicializálása
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == lastSceneName) return;
        lastSceneName = sceneName;

        if (sceneName == "MainMenu" || sceneName == "OptionsScene")
        {
            PlayMusic(menuMusic, 0.09f);
        }
        else if (sceneName == "EndScene")
        {
            PlayMusic(endSceneMusic, 0.05f);
        }
        else
        {
            PlayMusic(levelMusic, 0.05f);
        }
    }

    private void PlayMusic(AudioClip clip, float volume)
    {
        if (musicSource.clip == clip && Mathf.Approximately(musicSource.volume, volume))
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Publikus metódus hangeffektek lejátszására
    public void PlayGameOverSound()
    {
        musicSource.Stop();
        PlaySFX(gameOverSound, 0.2f);
    }

    public void PlayShipDestroySound()
    {
        PlaySFX(shipDestroySound, 0.3f);
    }

    private void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
