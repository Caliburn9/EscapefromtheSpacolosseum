using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenemySight : MonoBehaviour
{
    public float distance;



    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        
        if(hitInfo.collider !=null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        }

        if(hitInfo.collider.CompareTag("Player"))
        {
            Destroy(hitInfo.collider.gameObject);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }
    }
}
