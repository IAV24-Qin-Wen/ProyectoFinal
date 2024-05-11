using UnityEngine;
using UnityEngine.Rendering;

public class Debug_CameraMovement : MonoBehaviour
{
    public float Speed = 50;
    bool cenital=false;
    Camera camera;
    [SerializeField] GameObject postProcessing;
    Volume volume;
    void Start()
    {
        volume=postProcessing.GetComponent<Volume>();   
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        //MOVEMENT
        float xAxisValue = Input.GetAxis("Horizontal") * Speed;
        float zAxisValue = Input.GetAxis("Vertical") * Speed;

        //if (cenital)
        //{
        //    xAxisValue *= -1;
        //    zAxisValue *= -1;
        //}
     
        //ZOOM
        float yValue = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            if (cenital) camera.orthographicSize -= Speed;
            
            else yValue = -Speed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (cenital) camera.orthographicSize += Speed;
            else yValue = Speed;
        }

        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y + yValue, transform.position.z + zAxisValue);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cenital = !cenital;
            if (cenital)
            {
                transform.transform.eulerAngles = new Vector3(90, 0, 0);
                camera.orthographic = true;
                volume.weight = 0.15f;
                camera.orthographicSize = 35.26989f;
                transform.position = new Vector3(-6f, 19f, 0f);
            }
            else
            {
                transform.transform.eulerAngles = new Vector3(48.708f, 0, 0) ;
                camera.orthographic = false;
                volume.weight= 1.0f;
                transform.position = new Vector3(-5f, 34f, -36.5f);
            }

        }
    }
}