using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // freezes the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game...");
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // unfreezes the game
        isPaused = false;
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level...");
        Time.timeScale = 1f; // reset timescale before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Program...");
        Time.timeScale = 1f; // reset timescale before quitting
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

