using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu, optionsScreen, gameOverScreen;
    public Slider volumeSlider;
    public void Start()
    {
        GameManager.volume = volumeSlider.value;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            print("PAUSED");
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseMenu.SetActive(false);
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
    public void OptionsMenu()
    {
        pauseMenu.SetActive(false);
        optionsScreen.SetActive(true);
    }
    public void BackButton()
    {
            optionsScreen.SetActive(false);
            pauseMenu.SetActive(true);
    }
    public void VolumeAdjust()
    {
        GameManager.volume = volumeSlider.value;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Restart()
    {
        GameManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
