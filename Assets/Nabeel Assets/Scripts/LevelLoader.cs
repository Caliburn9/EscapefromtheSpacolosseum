using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Game1L1");
    }

    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
