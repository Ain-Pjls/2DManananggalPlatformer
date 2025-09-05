using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winPanel; // Assign your win panel (set it inactive at start)

    private void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false); // Hide at the start
    }

    public void ShowWinScreen()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void OnHomeButton()
    {
        Debug.Log("Exiting Program...");
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OnRestartButton()
    {
        Debug.Log("Restarting Level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLevelButton()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.Log("No next level available!");
    }
}

