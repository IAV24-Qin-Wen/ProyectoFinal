using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[Action("LiquidSnake/CheckRoomInfo")]
[Help("")]
public class CheckRoomInfo : GOAction
{
    [InParam("room")]
    [Help("Room you want to go to")]
    public int roomID;

    [OutParam("destRoomPosition")]
    [Help("output variable")]
    public Vector3 position;


    [OutParam("previousGO")]
    [Help("output variable")]
    public int previousGO;

    private RoomsPath.RoomInfo room;

    public override void OnStart()
    {
        room = gameObject.GetComponent<RoomsPath>().GetRooms()[roomID];
        base.OnStart();
        Debug.Log("Dest: "+roomID);
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        position = room.go.transform.position;
        previousGO = room.previousGo;

      
        return TaskStatus.COMPLETED;
    }
}
