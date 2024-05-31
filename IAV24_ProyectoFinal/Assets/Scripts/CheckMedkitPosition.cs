using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[Action("LiquidSnake/CheckMedkitPosition")]
[Help("")]
public class CheckMedkitPosition : GOAction
{
    [InParam("medkit")]
    [Help("Medkit you want to go to")]
    public int medkitID;

    ///<value>OutPut Boolean Parameter.</value>
    [OutParam("position")]
    [Help("output variable")]
    public Vector3 position;

    private RoomsPath.MedkitInfo medkit;

    public override void OnStart()
    {
        medkit = gameObject.GetComponent<RoomsPath>().GetMedkits()[medkitID];
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        position = medkit.go.transform.position;
        //Debug.Log(medkit.roomID);
        return TaskStatus.COMPLETED;
    }
}
