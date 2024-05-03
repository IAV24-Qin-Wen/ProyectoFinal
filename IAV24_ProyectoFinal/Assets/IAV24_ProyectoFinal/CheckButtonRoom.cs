using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[Action("IAV24/CheckButtonRoom")]
[Help("")]
public class CheckButtonRoom : GOAction
{
    [InParam("room")]
    [Help("Room you want to go to")]
    public int roomID;

    [OutParam("buttonRoomPosition")]
    [Help("output variable")]
    public Vector3 buttonRoomPosition;

    [OutParam("buttonGO")]
    [Help("output variable")]
    public int buttonGO;

    [OutParam("needToBeActivated")]
    [Help("output variable")]
    public bool needToBeActivated;


    private RoomsPath.RoomInfo room;

    public override void OnStart()
    {
        room = gameObject.GetComponent<RoomsPath>().GetRooms()[roomID];
        base.OnStart();
        Debug.Log("LAST: "+ roomID);
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        buttonGO = room.buttonGo;
        if (buttonGO != -1)
            buttonRoomPosition = gameObject.GetComponent<RoomsPath>().GetRooms()[buttonGO].go.transform.position;
        needToBeActivated = room.needToBeActivated;
   
        return TaskStatus.COMPLETED;
    }
}
