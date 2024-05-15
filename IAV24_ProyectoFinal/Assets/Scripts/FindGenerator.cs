using System.Collections.Generic;
using UnityEngine;
using static MapInfo;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class FindGenerator : Action
    {
        private float detectionAngles = 90f;

        private float sensorDepth = 8f;

        private float verticalOffset = 1f;

        [UnityEngine.Serialization.FormerlySerializedAs("levelMap")]
        public SharedGameObject mapinfo;

        public override TaskStatus OnUpdate()
        {
            GameObject generator;
            if ((generator = DetectClosestTarget())!= null)
            {
                //Debug.Log("Generator found");
                foreach(GeneratorInfo gen in mapinfo.Value.GetComponent<MapInfo>().generators) { 
                    if(ReferenceEquals(generator, gen))
                    {
                        gen.found = true;
                        break;
                    }
                }
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private GameObject DetectClosestTarget()
        {
            float minDistance = Mathf.Infinity;
            GameObject closest = null;

            // punto de origen de la visión habiendo aplicado el offset vertical
            // (desde aquí realizaremos el raycast para buscar objetos).
            Vector3 sightOrigin = gameObject.transform.position + Vector3.up * verticalOffset;

            var objects = GameObject.FindGameObjectsWithTag("Generator");
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
}