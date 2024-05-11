using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[Action("LiquidSnake/CheckPreviousRoom")]
[Help("Checks if the player is able to go to the room")]
public class CheckPreviousRoom : GOAction
{
    [InParam("room")]
    [Help("Room you want to go to")]
    public int actual_roomID;
    ///<value>OutPut Boolean Parameter.</value>
    [OutParam("previousRoom")]
    [Help("output variable")]
    public int previous_roomID;

    private RoomsPath.RoomInfo room;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        previous_roomID = gameObject.GetComponent<RoomsPath>().GetRooms()[actual_roomID].previousGo;

        return TaskStatus.COMPLETED;
    }
}
