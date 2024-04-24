using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchControlMode : MonoBehaviour
{
    bool autoMode = false;
    [SerializeField]
    BehaviorExecutor autoMovement;
    [SerializeField]
    BehaviorExecutor mouseMovement;
    NavMeshAgent nav;
    Animator animator;
    void Start()
    {
        nav=GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator=GetComponent<Animator>();

    }

    public bool IsAutoMode()
    {
        return autoMode;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            autoMode = !autoMode;
            mouseMovement.paused = autoMode;
            mouseMovement.enabled = !autoMode;
            autoMovement.paused = !autoMode;
            autoMovement.enabled = autoMode;
            nav.isStopped = !autoMode;
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 0);
        }



    }
}
