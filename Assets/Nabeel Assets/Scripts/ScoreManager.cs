using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;

    public static void increaseScore(int increment)
    {
        score += increment;
        Debug.Log("The score is:" + score);

        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
