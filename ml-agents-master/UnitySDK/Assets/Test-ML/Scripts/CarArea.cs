using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;

public class CarArea : Area
{
    
    public CarAgent carAgent;
    //public GameObject penguinBaby;

    //public PenguinFish fishPrefab;
    public Coin coinPrefab;

    public Text cumulativeRewardText;

    [HideInInspector]
    public float coinRadius = 1f;

    public List<GameObject> coinList;

    public override void ResetArea()
    {
        RemoveAllCoins();
        PlaceCar();
        SpawnCoins(10);
    }

    public void RemoveSpecificCoin(GameObject coinObject)
    {
        coinList.Remove(coinObject);
        Destroy(coinObject);
    }

    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;

        if (maxRadius > minRadius)
        {
            radius = UnityEngine.Random.Range(minRadius, maxRadius);
        }

        return center + Quaternion.Euler(0f, UnityEngine.Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
    }

    private void RemoveAllCoins()
    {
        if (coinList != null)
        {
            for (int i = 0; i < coinList.Count; i++)
            {
                if (coinList[i] != null)
                {
                    Destroy(coinList[i]);
                }
            }
        }

        coinList = new List<GameObject>();
    }

    private void PlaceCar()
    {
        carAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        carAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }

    private void SpawnCoins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject coinObject = Instantiate<GameObject>(coinPrefab.gameObject);
            coinObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            coinObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            coinObject.transform.parent = transform;
            coinList.Add(coinObject);
        }
    }

    private void Update()
    {
        cumulativeRewardText.text = carAgent.GetCumulativeReward().ToString("0.00");
    }
    
}
