using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class TestCarAgent : Agent
{
    public GameObject heartPrefab;
    public GameObject regurgitatedFishPrefab;

    private TestCarArea penguinArea;
    private Animator animator;
    private RayPerception3D rayPerception;
    //private GameObject baby;

    //private bool isFull; // If true, penguin has a full stomach

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Convert actions to axis values
        float forward = vectorAction[0];
        float leftOrRight = 0f;
        if (vectorAction[1] == 1f)
        {
            leftOrRight = -1f;
        }
        else if (vectorAction[1] == 2f)
        {
            leftOrRight = 1f;
        }

        // Set animator parameters
        animator.SetFloat("Vertical", forward);
        animator.SetFloat("Horizontal", leftOrRight);

        // Tiny negative reward every step
        AddReward(-1f / agentParameters.maxStep);
    }

    public override void AgentReset()
    {
        //isFull = false;
        penguinArea.ResetArea();
    }

    public override void CollectObservations()
    {
        // Has the penguin eaten
        //AddVectorObs(isFull);

        // Distance to the baby
        //AddVectorObs(Vector3.Distance(baby.transform.position, transform.position));

        // Direction to baby
        //AddVectorObs((baby.transform.position - transform.position).normalized);

        // Direction penguin is facing
        AddVectorObs(transform.forward);

        // RayPerception (sight)
        // ========================
        // rayDistance: How far to raycast
        // rayAngles: Angles to raycast (0 is right, 90 is forward, 180 is left)
        // detectableObjects: List of tags which correspond to object types agent can see
        // startOffset: Starting height offset of ray from center of agent
        // endOffset: Ending height offset of ray from center of agent
        float rayDistance = 20f;
        float[] rayAngles = { 30f, 60f, 90f, 120f, 150f };
        string[] detectableObjects = { "coin", "wall", "sand" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }

    private void Start()
    {
        penguinArea = GetComponentInParent<TestCarArea>();
        //baby = penguinArea.penguinBaby;
        animator = GetComponent<Animator>();
        rayPerception = GetComponent<RayPerception3D>();
    }

    private void FixedUpdate()
    {
        //if (Vector3.Distance(transform.position, baby.transform.position) < penguinArea.feedRadius)
        //{
            // Close enough, try to feed the baby
            //RegurgitateFish();
        //}
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("coin"))
        {
            EatFish(collision.gameObject);
        }


    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("coin"))
        {
            EatFish(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("sand"))
        {
            AddReward(-0.1f);
        }
    }

    private void EatFish(GameObject fishObject)
    {

        penguinArea.RemoveSpecificFish(fishObject);

        AddReward(1f);
    }





    //Agent brain
    public override float[] Heuristic()
    {


        float[] playerInput = { 0f, 0f };

        if (Input.GetKey(KeyCode.W))
        {
            playerInput[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerInput[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerInput[1] = 2;
        }
        return playerInput;
    }
}
