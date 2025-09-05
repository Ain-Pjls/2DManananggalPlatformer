using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    public ExitDoor fireboyDoor;
    public ExitDoor watergirlDoor;
    public GameObject winScreenUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckWinCondition()
    {
        if (fireboyDoor.IsPlayerOnDoor() && watergirlDoor.IsPlayerOnDoor())
        {
            Debug.Log("Both players reached their doors! YOU WIN!");

            if (winScreenUI != null)
                winScreenUI.SetActive(true);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Next level fallback
        }
    }
}

