using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update
    private float zoom = 40f;
    private float zoomAmount = 160f;
    private CinemachineVirtualCamera m_Camera;


    private void Start()
    {
        m_Camera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
            zoom -= zoomAmount * Time.deltaTime;
        if (Input.mouseScrollDelta.y < 0)
            zoom += zoomAmount * Time.deltaTime;

        zoom = Mathf.Clamp(zoom, 5f, 50f);
        m_Camera.m_Lens.FieldOfView = zoom;

    }
}
