using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class player : Agent
{
    
    Rigidbody rBody;
    Vector3 target;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        target = new Vector3(5f, 2f, 5f);
    }

    public override void OnEpisodeBegin()
    {
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-5f, 2f, -5f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // 1 branch, 4 actions
        // number from 0 - 3
        int direction = actionBuffers.DiscreteActions[0];
        Debug.Log("Action received "+ direction);
        if (direction == 0)
        {
            transform.position += Vector3.left; 
        }
        if (direction == 1)
        {
            transform.position += Vector3.right;
        }
        if (direction == 2)
        {
            transform.position += Vector3.forward;
        }
        if (direction == 3)
        {
            transform.position += Vector3.back;
        }
        float xPos = transform.position[0];
        float zPos = transform.position[2];
        xPos = Mathf.Clamp(xPos, -5f, 5f);
        zPos = Mathf.Clamp(zPos, -5f, 5f);
        this.transform.position = new Vector3(xPos, 2f, zPos);

        // check reward
        if (this.transform.position == target)
        {
            SetReward(1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int action = 0;
        var continuousActionsOut = actionsOut.DiscreteActions;
        action = Input.GetAxis("Horizontal") > 0? 0: 1;
        action = Input.GetAxis("Vertical") > 0? 2: 3;
        // if (Input.GetKey(KeyCode.F))
        // {
        //     action = 0;
        // }
        // if (Input.GetKey(KeyCode.H))
        // {
        //     action = 1;
        // }
        // if (Input.GetKey(KeyCode.T))
        // {
        //     action = 2;
        // }
        // if (Input.GetKey(KeyCode.G))
        // {
        //     action = 3;
        // }
        Debug.Log("Calling heuristics " + action);
        continuousActionsOut[0] = action;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         transform.position += Vector3.left; 
    //     }
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         transform.position += Vector3.right;
    //     }
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         transform.position += Vector3.forward;
    //     }
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         transform.position += Vector3.back;
    //     }
    //     float xPos = transform.position[0];
    //     float zPos = transform.position[2];
    //     xPos = Mathf.Clamp(xPos, -5f, 5f);
    //     zPos = Mathf.Clamp(zPos, -5f, 5f);
    //     transform.position = new Vector3(xPos, transform.position[1], zPos);
    // }
}
