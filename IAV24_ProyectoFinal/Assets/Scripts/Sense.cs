using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Sense : MonoBehaviour
{
    public float fov;
    public float fovAngle;

    protected List<GameObject> gameObjects;

    protected List<GameObject> allGameObjects;


    [SerializeField] private Color areaColor = new Color(0f, 1f, 0f, 0.2f);

    [Tooltip("Mostrar objetivo")]
    public bool showArea = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showArea)
        {
            Handles.color = areaColor;
            Handles.DrawSolidArc(transform.position, transform.up,
                UnityEngine.Quaternion.AngleAxis(-fovAngle / 2f, transform.up) * transform.forward,
                fovAngle, fov);
        }
    }
#endif

    private void Start()
    {
        gameObjects = new List<GameObject>();
        allGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        gameObjects.Clear();
        allGameObjects.Clear();
        Collider[] cols = Physics.OverlapSphere(transform.position, fov);
        foreach (Collider c in cols)
        {
            if (c.gameObject == this.gameObject) continue;
            //angulo de objeto segun posicion de transform
            float angle = UnityEngine.Vector3.Angle(transform.forward, c.transform.position - transform.position);
            //si esta en area de vision
            if (Mathf.Abs(angle) < fovAngle / 2)
            {
                gameObjects.Add(c.gameObject);
                //UnityEngine.Debug.Log(c.name);
            }

            allGameObjects.Add(c.gameObject);
        }
    }

    public List<GameObject> getDetectedGameObjects()
    {
        return gameObjects;
    }

    public List<GameObject> getAllGameObjects()
    {
        return allGameObjects;
    }

}
