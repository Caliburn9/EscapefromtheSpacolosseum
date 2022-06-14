using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int index;
    public string levelName;

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(index);
            index++;
            Debug.Log("test");

           // SceneManager.LoadScene("Level1");
        }
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
