using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public int spawnAmount;
    float time = 0.0f;
    public float timer;
    float minTimer, maxTimer;
    public GameObject Obstacle;
    public Transform spawnLocation;
    public bool obstisRight;

    // Start is called before the first frame update
    private void Start()
    {
        minTimer = timer - 2;
        maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            time = 0.0f;
            timer = Random.Range(minTimer, maxTimer);

            if (spawnAmount != 0)
            {
                spawnObstacle(Obstacle, spawnLocation);
                spawnAmount--;
                Obstacle.GetComponent<ObstacleAI>().moveRight = obstisRight;
            }
        }
    }

    void spawnObstacle(GameObject obst, Transform spawnLoc)
    {
        Instantiate(obst, spawnLoc.transform.position, Quaternion.identity);
        
    }
}
