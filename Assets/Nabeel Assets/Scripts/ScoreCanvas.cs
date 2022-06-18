using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCanvas : MonoBehaviour
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
        textDisplay.text = "Score: " + ScoreManager.score.ToString(); 
    }
}
