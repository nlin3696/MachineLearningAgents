using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro; //text mesh pro
using System;

public class PenguinArea : Area  //inheirt from area
{
    public PenguinAgent penguinAgent;
    public GameObject penguinBaby;
    public PenguinFish fishPrefab;
    public TextMeshPro cumlativeRewardText;

    [HideInInspector]
    public float fishSpeed = 0f;
    [HideInInspector]
    public float feedRadius = 1f;

    private List<GameObject> fishList;

    public override void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlaceBaby();
        SpawnFish(4, fishSpeed);
    }

    public void RemoveSpecificFish(GameObject fishObject)
    {
        fishList.Remove(fishObject);
        Destroy(fishObject);
    }

    //choosing random position in a circle given a center
    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;

        if(maxRadius > minRadius)
        {
            radius = UnityEngine.Random.Range(minRadius, maxRadius);
        }

        return center + Quaternion.Euler(0f, UnityEngine.Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
    }

    private void RemoveAllFish()
    {
        if (fishList != null)
        {
            for(int i = 0; i < fishList.Count; i++)
            {
                if (fishList[i] != null)
                {
                    Destroy(fishList[i]);
                }
            }
        }

        fishList = new List<GameObject>();
    }

    private void PlacePenguin()
    {
        penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * 0.5f;
        penguinAgent.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0f);
    }

    private void PlaceBaby()
    {
        penguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * 0.5f;
        penguinBaby.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void SpawnFish(int count, float fishSpeed)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject fishObject = Instantiate<GameObject>(fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * 0.5f;
            fishObject.transform.rotation = Quaternion.Euler(0f, 360f, 0f);
            fishObject.transform.parent = transform;
            fishList.Add(fishObject);
            fishObject.GetComponent<PenguinFish>().fishSpeed = fishSpeed;
        }
    }

    private void Update()
    {
        cumlativeRewardText.text = penguinAgent.GetCumulativeReward().ToString("0.00");
    }
}
