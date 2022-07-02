using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame1()
    {
        SceneManager.LoadScene("Game1L1");
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene("Game2L1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
