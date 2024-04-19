using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

[Action("IAV24/CheckPath")]
[Help("Checks if the player is able to go to the position")]
public class CheckPath : GOAction
{
    [InParam("dest")]
    [Help("dest position")]
    public Vector3 position;


    ///<value>OutPut Boolean Parameter.</value>
    [OutParam("var")]
    [Help("output variable")]
    public bool var;

    private NavMeshAgent navAgent;

    public override void OnStart()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        NavMeshPath path = new NavMeshPath();

        navAgent.CalculatePath(position, path);
        switch (path.status)
        {
            case NavMeshPathStatus.PathComplete:
                //Debug.Log($"Robot will be able to reach room.");
                var = true;
                break;
            case NavMeshPathStatus.PathPartial:
                //Debug.LogWarning($"Robot will only be able to move partway.");
                var = false;
                break;
            default:
                //Debug.LogError($"There is no valid path for Robot to reach.");
                var = false;
                break;
        }

        //Debug.Log(position);
        return TaskStatus.COMPLETED;
    }

}
