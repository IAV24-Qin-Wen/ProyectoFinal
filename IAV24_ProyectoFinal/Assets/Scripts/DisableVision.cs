using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidSnake.Enemies
{
    public class DisableVision : MonoBehaviour
    {
        //[SerializeField]
        //private Material[] mat;
        // Lista de enemigos
        private GameObject[] enemyVisionList;
        private GameObject[] enemyList;

        void Start()
        {
            // Inicialización de la lista de enemigos (Parte de la Vision)
            if (enemyVisionList == null)
                enemyVisionList = GameObject.FindGameObjectsWithTag("Vision");

            // Inicialización de la lista de enemigos
            if (enemyList == null)
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //Debug.Log("Vision desactivada");
				
                // Se desactiva la visión de los enemigos, de modo que no pueden detectar al jugador
                foreach (GameObject enemy in enemyVisionList)
                {
                    enemy.GetComponent<VisionSensor>().enabled = false;
                    //enemy.GetComponent<MeshRenderer>().material. = "VisionConeMaterial";
                }
                // Se desactiva la capacidad de disparar de los enemigos, de modo que no pueden detectar al jugador
                foreach (GameObject enemy in enemyList)
                {
                    enemy.GetComponent<LaserShooter>().enabled = false;
                }
            }
        }


        void OnTriggerExit(Collider other)
        {        
            if(other.tag == "Player")
            {
                //Debug.Log("Vision activada");

                // Se activa la visión de los enemigos, de modo que pueden detectar al jugador
                foreach (GameObject enemy in enemyVisionList)
                {
                    enemy.GetComponent<VisionSensor>().enabled = true;
                }
                // Se activa la capacidad de disparar de los enemigos, de modo que pueden detectar al jugador
                foreach (GameObject enemy in enemyList)
                {
                    enemy.GetComponent<LaserShooter>().enabled = true;
                }
            }      
        }
        
    }
}