using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("LiquidSnake/FindButtons")]
[Help("")]
public class FindButtons : GOAction
{
    private float detectionAngles = 90f;

    private float sensorDepth = 8f;

    private float verticalOffset = 1f;

    [OutParam("buttonPosition")]
    [Help("output variable")]
    public Vector3 buttonPos;
    
    [OutParam("buttonGO")]
    [Help("output variable")]
    public GameObject button;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        button = DetectClosestTarget();
        if (button != null)
        {
            buttonPos = button.transform.position;
            buttonPos.y = 0.12f;
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }

    private GameObject DetectClosestTarget()
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;

        // punto de origen de la visión habiendo aplicado el offset vertical
        // (desde aquí realizaremos el raycast para buscar objetos).
        Vector3 sightOrigin = gameObject.transform.position + Vector3.up * verticalOffset;

        var objects = GameObject.FindGameObjectsWithTag("Button");
        foreach (var obj in objects)
        {
            Vector3 targetPos = obj.GetComponent<Collider>().bounds.center;
            Vector3 dir = targetPos - sightOrigin;
            Vector3 planarDir = new Vector3(dir.x, 0f, dir.z);

            // Check de distancia: no nos interesa nada que sobrepase la distancia de detección
            if (planarDir.sqrMagnitude > sensorDepth * sensorDepth) continue;

            if (Mathf.Abs(Vector3.Angle(gameObject.transform.forward, planarDir)) < detectionAngles / 2)
            {
                RaycastHit hit;
                // TODO: soporte para LayerMask
                if (Physics.Raycast(sightOrigin, dir, out hit))
                {
                    // No hay nada que obstruya la visión desde nuestro punto hasta el objeto,
                    // y además la distancia al objeto en cuestión es menor que la mínima registrada.
                    if (hit.collider.gameObject == obj)
                    {
                        float d = dir.sqrMagnitude;
                        if (d < minDistance)
                        {
                            minDistance = d; closest = obj;

                            break;
                        }
                    }
                }
            }
        }
        return closest;
    } 
} 
