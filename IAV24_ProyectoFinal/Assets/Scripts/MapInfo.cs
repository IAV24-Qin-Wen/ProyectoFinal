using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using LiquidSnake.Character;

public class MapInfo : MonoBehaviour
{
    int nRepaired = 0;
    [System.Serializable]
    public class HookInfo
    {
        public GameObject go;
        public bool used;
    }

    [System.Serializable]
    public class GeneratorInfo
    {
        public GameObject go;
        public int ID;
        public bool found;
        public Progress progress;
    }
    [SerializeField]
    public List<GeneratorInfo> generators;
    public SharedTransformList sharedTransformList;
    public List<HookInfo> hooks;
    public GameObject destination;
    public GameObject doors;

    void Start()
    {
        sharedTransformList = new SharedTransformList();
        for (int i = 0; i < generators.Count; ++i)
        {
            sharedTransformList.Value.Add(generators[i].go.transform);
        }
        Debug.Log("fjfj " + sharedTransformList.Value.Count);

    }
    public void OnGeneratorRepaired()
    {
        nRepaired++;
        if(nRepaired == generators.Count)
        {
            doors.SetActive(false);
        }
    }

}
