using BBUnity.Actions;
using LiquidSnake.LevelObjects;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.Events;

[Action("LiquidSnake/UseButton")]
[Help("")]
public class UseButton : GOAction
{
    [InParam("button")]
    [Help("input variable")]
    public GameObject button;
    
    [InParam("destRoom")]
    [Help("input variable")]
    public int destRoom;

    //private UnityEvent onAreaEntered;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        button.GetComponent<ButtonAreaTrigger>().toggle();

        RoomsPath.RoomInfo aux = gameObject.GetComponent<RoomsPath>().GetRooms()[destRoom];
        aux.activated = !aux.activated;

       // Debug.Log(destRoom);
        return TaskStatus.COMPLETED;
    }
}