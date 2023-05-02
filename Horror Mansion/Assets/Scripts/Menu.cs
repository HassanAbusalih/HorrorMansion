using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is for the buttons in the main menu screen, and contains methods for starting and exiting the game.
/// </summary>

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
