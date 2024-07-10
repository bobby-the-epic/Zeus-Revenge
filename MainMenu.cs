using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen, optionsScreen, backButton;
    public Button playButton, optionsButton, quitButton;
    public Slider volumeSlider;

    AudioSource mainMenuAudio;

    void Start()
    {
        mainMenuAudio = GetComponent<AudioSource>();
        mainMenuAudio.Play();
    }
    void Update()
    {
        mainMenuAudio.volume = GameManager.volume;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Zeus' Revenge");
    }
    public void OptionsMenu()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
        backButton.SetActive(true);
    }
    public void VolumeAdjust()
    {
        GameManager.volume = volumeSlider.value;
    }
    public void BackButton()
    {
        optionsScreen.SetActive(false);
        titleScreen.SetActive(true);
        backButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
