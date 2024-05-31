using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;


[Action("LiquidSnake/Rotate")]
[Help("Smoothly rotates the character in a random direction and degrees")]
public class Rotate : GOAction
{
    private float duration = 1.5f;
    private Quaternion startRotation;
    private float t;

    [InParam("buttonPosition")]
    [Help("input variable")]
    public GameObject button;

    [InParam("finishedRotating")]
    [Help("input variable")]
    public bool finishedRotating;

    public override void OnStart()
    {
        base.OnStart();
        startRotation = gameObject.transform.rotation;
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();


        if (button != null) {
            return TaskStatus.COMPLETED; 
        }

        t += Time.deltaTime;

        gameObject.transform.rotation = startRotation * Quaternion.AngleAxis(t / duration * 360f, Vector3.up);

        if (t < duration)
        {
            return TaskStatus.RUNNING;
        }

        if(gameObject.transform.rotation.y > 360.0f)
            gameObject.transform.rotation = startRotation;

        return TaskStatus.COMPLETED;
    }
}
