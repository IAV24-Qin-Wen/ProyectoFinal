using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorList : MonoBehaviour
{
    [System.Serializable]
    public class MedkitInfo
    {
        public GameObject go;
        public int roomID;
        public bool used;
    }

    [System.Serializable]
    public class RoomInfo
    {
        public GameObject go;
        public int previousGo;
        public int buttonGo;
        public bool needToBeActivated;
        public bool activated;

    }
    [SerializeField]
    private List<RoomInfo> rooms;

    [SerializeField]
    private List<MedkitInfo> medkits;

    public List<RoomInfo> GetRooms() { return rooms; }
    public List<MedkitInfo> GetMedkits() { return medkits; }

}
