using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[Action("LiquidSnake/CheckPreviousMedkit")]
[Help("Checks if the player is able to go to the medkit")]
public class CheckPreviousMedkit : GOAction
{
    [InParam("medkit")]
    [Help("Medkit you want to go to")]
    public int actual_MedkitID;
    ///<value>OutPut Boolean Parameter.</value>
    [OutParam("previousMedkit")]
    [Help("output variable")]
    public int previous_medkitID;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

       // previous_medkitID = gameObject.GetComponent<RoomsPath>().GetMedkits()[previous_medkitID].previousGo;

        //Debug.Log("Medkit: " +  previous_medkitID);

        return TaskStatus.COMPLETED;
    }
}
