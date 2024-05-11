using LiquidSnake.Utils;
using UnityEngine;


namespace LiquidSnake.Character
{
    public class PlayerVisionSensor : MonoBehaviour, IResetteable
    {
        //----------------------------------------------------------------------------
        //                      Atributos de Inspector
        //----------------------------------------------------------------------------
        #region Atributos de Inspector
        [SerializeField]
        [Tooltip("Range of detection, in degrees.")]
        private float detectionAngles = 90f;

        [SerializeField]
        [Tooltip("Depth of the detection area.")]
        private float sensorDepth = 5f;

        [SerializeField]
        [Tooltip("Vertical offset of the detection area.")]
        private float verticalOffset = 0f;

        [SerializeField]
        private string[] detectableTags;

        #endregion


        //----------------------------------------------------------------------------
        //                      Atributos Privados
        //----------------------------------------------------------------------------
        /// <summary>
        /// elemento detectado más cerca del punto de origen del sensor.
        /// Si no se ha detectado nada aún, este valor es null.
        /// </summary>
        private GameObject _closestTarget;

        //----------------------------------------------------------------------------
        //                       Ciclo de vida del componente
        //----------------------------------------------------------------------------

        private void FixedUpdate()
        {
            // en cada actualización de físicas recalculamos el nuevo objetivo más cercano.
            // TODO: Dar un poco más de control sobre frecuencia de detección y otros parámetros
            _closestTarget = DetectClosestTarget();
        } // FixedUpdate

        /// <summary>
        /// Devuelve el objeto detectado más cercano al punto de origen del sensor
        /// (esto implica que el objeto sea visible y no sólo que se encuentre dentro
        /// del área del mesh). Si el resultado de esta operación no es null, modifica
        /// el material del mesh renderer con el material de alerta para notificar de que
        /// el sensor está percibiendo al objetivo.
        /// </summary>
        private GameObject DetectClosestTarget()
        {
            float minDistance = Mathf.Infinity;
            GameObject closest = null;

            // punto de origen de la visión habiendo aplicado el offset vertical
            // (desde aquí realizaremos el raycast para buscar objetos).
            Vector3 sightOrigin = transform.position + Vector3.up * verticalOffset;

            foreach (string tag in detectableTags)
            {
                // TODO: Ahora mismo esta comprobación es barata porque hay un único elemento con tag Player
                // pero si cambia esta asunción puede llegar a ser una operación muy cara...
                var objects = GameObject.FindGameObjectsWithTag(tag);
                foreach (var obj in objects)
                {
                    Vector3 targetPos = obj.GetComponent<Collider>().bounds.center;
                    Vector3 dir = targetPos - sightOrigin;
                    Vector3 planarDir = new Vector3(dir.x, 0f, dir.z);

                    // Check de distancia: no nos interesa nada que sobrepase la distancia de detección
                    if (planarDir.sqrMagnitude > sensorDepth * sensorDepth) continue;

                    if (Mathf.Abs(Vector3.Angle(transform.forward, planarDir)) < detectionAngles / 2)
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
                                    Debug.Log("BOTON DETECTADO");
                                }
                            }
                        }
                    }
                }
            }
            return closest;
        } // DetectClosestTarget

        //----------------------------------------------------------------------------
        //                       API Pública del componente
        //----------------------------------------------------------------------------

        public void Reset()
        {
            _closestTarget = null;
        } // Reset

        public GameObject GetClosestTarget()
        {
            return _closestTarget;
        } // GetClosestTarget


    } // PlayerVisionSensor

} // namespace LiquidSnake.Enemies