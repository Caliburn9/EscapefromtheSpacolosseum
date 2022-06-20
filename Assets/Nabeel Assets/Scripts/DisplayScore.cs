using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            textDisplay.text = textDisplay.text = "Score: " + ScoreManager.score.ToString() + "\n" + "HighScore: " + PlayerPrefs.GetInt("Highscore");
        } else
        {
            textDisplay.text = "Score: " + ScoreManager.score.ToString();
        }
    }
}
