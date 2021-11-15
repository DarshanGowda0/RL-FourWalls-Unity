using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position += Vector3.left; 
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.position += Vector3.back;
        }
        float xPos = transform.position[0];
        float zPos = transform.position[2];
        xPos = Mathf.Clamp(xPos, -5f, 5f);
        zPos = Mathf.Clamp(zPos, -5f, 5f);
        transform.position = new Vector3(xPos, transform.position[1], zPos);
    }
}
