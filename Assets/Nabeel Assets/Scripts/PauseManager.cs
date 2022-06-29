using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Canvas PauseCanvas;
    public LevelManager levelManager;
    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPaused = !isPaused;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (isPaused == true)
        {
            PauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
