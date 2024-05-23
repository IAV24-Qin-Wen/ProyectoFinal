using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

public class HookComponent : MonoBehaviour
{
    [SerializeField]
    private MapInfo m_MapInfo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Survivor")
        {        
            foreach (MapInfo.HookInfo m in m_MapInfo.hooks)
            {
                if(m.used && ReferenceEquals(gameObject, m.go))
                {
                    m.hookedSurvivor.GetComponent<BehaviorTree>().SendEvent<object>("Rescued", false);
                    m.used = false;
                    m.hookedSurvivor = null;
                }
            }
        }
    }
}
