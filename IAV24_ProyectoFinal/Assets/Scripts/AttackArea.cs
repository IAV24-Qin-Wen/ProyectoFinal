using System.Collections.Generic;
using TMPro;
using UnityEngine;
using LiquidSnake.Enemies;
public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private string targetTag;
    public GameEnding gameEnding;
    [SerializeField]
    private VisionSensor vision;
    bool isTargetInaArea;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isTargetInaArea = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isTargetInaArea = true;
        }
    }

    void OnTriggerExit(Collider other)

    {
        if (other.CompareTag(targetTag))
        {
            isTargetInaArea = false;
        }
    }

    void Update()
    { GameObject go;
        if (isTargetInaArea && (go=vision.GetClosestTarget())!=null)
        {
       
            Vector3 direction =go.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.CompareTag(targetTag))
                {
                    Debug.Log("Attack");
                    //gameEnding.CaughtPlayer();
                }
            }
        }
    }
}

