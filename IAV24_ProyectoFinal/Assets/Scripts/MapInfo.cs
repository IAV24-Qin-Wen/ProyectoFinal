using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class MapInfo : MonoBehaviour
{
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
    }
    [SerializeField]
    public List<GeneratorInfo> generators;
    public SharedTransformList sharedTransformList;
    public List<HookInfo> hooks;
    void Start()
    {
        sharedTransformList = new SharedTransformList();
        for (int i = 0; i < generators.Count; ++i)
        {
            sharedTransformList.Value.Add(generators[i].go.transform);
        }
        Debug.Log("fjfj " + sharedTransformList.Value.Count);

    }


}
