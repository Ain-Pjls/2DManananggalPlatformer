using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public PlayerType requiredPlayer; // Which player should stand on this door
    private bool playerOnDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Something entered {gameObject.name}: {collision.name}");

        Characters character = collision.GetComponent<Characters>();
        if (character != null)
        {
            Debug.Log($"{character.playerType} entered {requiredPlayer} door");

            if (character.playerType == requiredPlayer)
            {
                playerOnDoor = true;
                WinManager.Instance.CheckWinCondition();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"Something exited {gameObject.name}: {collision.name}");

        Characters character = collision.GetComponent<Characters>();
        if (character != null)
        {
            Debug.Log($"{character.playerType} exited {requiredPlayer} door");

            if (character.playerType == requiredPlayer)
            {
                playerOnDoor = false;
                WinManager.Instance.CheckWinCondition();
            }
        }
    }

    public bool IsPlayerOnDoor()
    {
        return playerOnDoor;
    }
}

