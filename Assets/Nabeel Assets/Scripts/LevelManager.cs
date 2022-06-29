using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string NextLevel;
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(NextLevel));
    }

    public void Restart()
    {
        StartCoroutine(LoadLevel("Game1L1"));
    }

    public void ReturntoMainMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void ScoreMenu()
    {
        StartCoroutine(LoadLevel("Game1Score"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
