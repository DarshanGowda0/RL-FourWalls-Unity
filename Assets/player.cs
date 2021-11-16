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

    public override void Initialize()
    {
        
        this.MaxStep = 10000;
        
    }

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        target = new Vector3(5f, 0.5f, 5f);
    }

    public override void OnEpisodeBegin()
    {
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-5f, 0.5f, -5f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);


        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target);

        // Reached target
        if (distanceToTarget < .5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        if (this.MaxStep > 0) 
            AddReward(-1f / this.MaxStep);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
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
