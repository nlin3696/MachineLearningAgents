using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CarAgent : Agent
{
    
    //public GameObject heartPrefab;
    //public GameObject regurgitatedFishPrefab;

    //private PenguinArea penguinArea;
    private CarArea carArea;

    private Animator animator;

    private RayPerception3D rayPerception;
    //private GameObject coin;
    public List<GameObject> coins;

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
        carArea.ResetArea();
    }

    public override void CollectObservations()
    {
        /*
        // Distance to the coins and direction to the coins
        for (int i = 0; i < coins.Length; i++)
        {
            AddVectorObs(Vector3.Distance(coins[i].transform.position, transform.position));
            AddVectorObs((coins[i].transform.position - transform.position).normalized);
        }   */ 

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
        string[] detectableObjects = { "coin", "sand" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }

    private void Start()
    {
        //penguinArea = GetComponentInParent<PenguinArea>();
        carArea = GetComponentInParent<CarArea>();

        //baby = penguinArea.penguinBaby;
        coins = carArea.coinList;

        //animator = GetComponent<Animator>();
        rayPerception = GetComponent<RayPerception3D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("coin"))
        {
            EatCoin(collision.gameObject);
        }
    }

    private void EatCoin(GameObject coinObject)
    {

        carArea.RemoveSpecificCoin(coinObject);

        AddReward(1f);
    }

    //Agent brain
    public override float[] Heuristic()
    {


        float[] playerInput = { 0, 0 };

        if (Input.GetMouseButtonDown(0))
        {
            playerInput[0] = 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerInput[0] = 2;
        }
        //
        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerInput[1] = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            playerInput[1] = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            playerInput[1] = 3;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            playerInput[1] = 4;
        }
        if (Input.GetKey(KeyCode.E))
        {
            playerInput[1] = 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerInput[1] = 6;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerInput[1] = 7;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerInput[1] = 8;
        }
        return playerInput;
    }
}
