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
    private List<GeneratorInfo> generators;
    public SharedTransformList sharedTransformList;
   [SerializeField]
    private List<HookInfo> hooks;
    void Start()
    {
        sharedTransformList = new SharedTransformList();
        for (int i = 0; i < generators.Count; ++i)
        {
            sharedTransformList.Value.Add(generators[i].go.transform);
        }
        Debug.Log("fjfj "+ sharedTransformList.Value.Count);

    }
    public List<GeneratorInfo> GetGenerators() { return generators; }

    public List<HookInfo> GetHooks() { return hooks; }

}
