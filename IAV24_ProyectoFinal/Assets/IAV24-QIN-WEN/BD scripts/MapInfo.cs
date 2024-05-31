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
        public GameObject hookedSurvivor;
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
    public SharedTransformList sharedGenTransformList;
    public SharedTransformList sharedHookTransformList;
    public List<HookInfo> hooks;
    public GameObject destination;
    public GameObject doors;
    public SurvivorManager survivorManager;

    void Awake()
    {
        sharedGenTransformList = new SharedTransformList();
        for (int i = 0; i < generators.Count; ++i)
        {
            sharedGenTransformList.Value.Add(generators[i].go.transform);
        }
        sharedHookTransformList = new SharedTransformList();
        for (int i = 0; i < hooks.Count; ++i)
        {
            sharedHookTransformList.Value.Add(hooks[i].go.transform);
        }

    }
    public void OnGeneratorRepaired()
    {
        nRepaired++;

        if(nRepaired == generators.Count)
        {
            doors.SetActive(false);
            survivorManager.gensRepaired();
        }
    }


}
