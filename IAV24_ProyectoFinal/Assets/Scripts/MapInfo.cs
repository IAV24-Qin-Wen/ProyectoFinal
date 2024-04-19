using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public float progress;
    }
    [SerializeField]
    private List<GeneratorInfo> generators;

    [SerializeField]
    private List<HookInfo> hooks;

    public List<GeneratorInfo> GetGenerators() { return generators; }
    public List<HookInfo> GetHooks() { return hooks; }

}
