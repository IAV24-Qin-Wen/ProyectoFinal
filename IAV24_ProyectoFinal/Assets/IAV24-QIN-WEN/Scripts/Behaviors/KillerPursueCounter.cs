using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
public class KillerPursueCounter : MonoBehaviour
{
    BehaviorTree behaviorTree;
    [SerializeField]
    bool tryingPursue = false;
    [SerializeField]
    float counter = 0;
    [SerializeField]
    bool lastPursue = false;

    [SerializeField]
    float maxPursuingTime = 0;

  
    // Update is called once per frame
    public void OnEnable()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.RegisterEvent<object>("DetectSurvivor", ReceivedEvent);
        behaviorTree.RegisterEvent("AttackedEvent", ReceivedEvent2);

    }


    public void Update()
    {
        if (tryingPursue)
        {
            counter += Time.deltaTime;
            if (counter > maxPursuingTime)
            {
                tryingPursue = false;
                behaviorTree.SendEvent<object>("TryingPursuing", null);
                counter = 0;
                
            }
        }


    }

    public void ReceivedEvent(object arg1)
    {
        if ((GameObject)arg1 == null)
        {
            if (lastPursue &&!tryingPursue)
            {
                tryingPursue = true;
                counter = 0;
                lastPursue = false;
            }
        }
        else
        {

            counter = 0;
            lastPursue = true;
        }
    }

    public void ReceivedEvent2()
    {
   
        lastPursue = false;
        counter = 0;
        tryingPursue = false;
    }


    public void OnDisable()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.UnregisterEvent<object>("DetectSurvivor", ReceivedEvent);
        behaviorTree.UnregisterEvent("AttackedEvent", ReceivedEvent2);
    }
}
