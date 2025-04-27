using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 
using UnityEngine.SceneManagement; // Provides scene loading and management pre-built functions to use

public class MainMenu : MonoBehaviour // Inherits the Built-In unity functions
{
    public void GameButtonStart() // Loads the scene through name
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GameButtonReMainMenu() // Loads back the Main Menu
    {
        SceneManager.LoadScene("MainMenuWithLoadMenu");
    }

    public void GameButtonQuit() // Exits the application
    {
        Application.Quit();
    }
}
