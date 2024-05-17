using LiquidSnake.Management;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SurvivorManager : MonoBehaviour
{
    [SerializeField]
    private int nSurvivors;
    [SerializeField]
    private GameObject survivorPrefab;
    [SerializeField]
    private GameObject survivorsParent;

    public List<GameObject> survivors;

    [SerializeField]
    private GameObject levelGo;
    private GameEnding level;

    int minSurvive;
    int survived;

    // Start is called before the first frame update
    void Start()
    {
        level=levelGo.GetComponent<GameEnding>();
        survivors =new List<GameObject> ();
        if (getNSurvivors() > 2) minSurvive = 2;
        else minSurvive = 1;

        GameObject spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoint");
        bool[] used = new bool[spawnPoints.transform.childCount];

        if (spawnPoints.transform.childCount < nSurvivors) return;

        for (int i = 0; i < nSurvivors; i++)
        {
            bool spawned = false;
            while (!spawned)
            {
                //aux = UnityEngine.Random.Range(0, spawnPoints.transform.childCount);

                //if (!used[aux])
                //{
                //    used[aux] = spawned = true;
                //    Instantiate(survivorPrefab, spawnPoints.transform.GetChild(aux).transform.position, 
                //        spawnPoints.transform.GetChild(aux).transform.rotation, survivors.transform);
                //}

                if (!used[i])
                {
                    used[i] = spawned = true;
                    survivors.Add(Instantiate(survivorPrefab, spawnPoints.transform.GetChild(i).transform.position,
                        spawnPoints.transform.GetChild(i).transform.rotation, survivorsParent.transform));
                }
            }

        }
    }
    public void OnSurvivorArrive(GameObject s)
    {
        survivors.Remove(s);
        Destroy(s);
        ++survived;
    }
    public void OnSurvivorDie(GameObject s)
    {
        if (s == null) return;
        survivors.Remove(s);
        Destroy(s);

        if (survivors.Count == 0 && survived<minSurvive) level.OnKillerWin();
    }
    public int getNSurvivors() { return nSurvivors; }
}
