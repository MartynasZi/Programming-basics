using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [Header("Scene Settings")]
    public string menuScene;

    [Header("Pause Menu Settings")]
    public GameObject pauseScreen;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    private bool isPaused = false;
    public KeyCode pauseKey = KeyCode.Escape;

    public void GoToMainMenu ()
    {
        
        SceneManager.LoadScene(menuScene);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void Update ()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }
    public void TogglePauseMenu()
    {
        if (optionsMenu != null && optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            if (pauseMenu != null) pauseMenu.SetActive(true);
            return;
        }
        else
        {
            isPaused = !isPaused;
            if (pauseScreen != null) pauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
            Cursor.visible = isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }    
}
