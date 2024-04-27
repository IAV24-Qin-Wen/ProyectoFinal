using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SurvivorManager : MonoBehaviour
{
    [SerializeField]
    private int nSurvivors;
    [SerializeField]
    private GameObject survivorPrefab;


    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoint");
        bool[] used = new bool[spawnPoints.transform.childCount];
        int aux;

        if (spawnPoints.transform.childCount < nSurvivors) return;

        for (uint i = 0; i < nSurvivors; i++)
        {
            bool spawned = false;
            while (!spawned)
            {
                aux = UnityEngine.Random.Range(0, spawnPoints.transform.childCount);
                Debug.Log(aux);
                if (!used[aux])
                {
                    used[aux] = spawned = true;
                    Instantiate(survivorPrefab, spawnPoints.transform.GetChild(aux).transform.position, spawnPoints.transform.GetChild(aux).transform.rotation);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
