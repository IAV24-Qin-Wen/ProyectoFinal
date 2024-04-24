using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;

namespace LiquidSnake.Enemies
{
    [Action("IAV24/AttackSurvivor")]
    public class AttackSurvivor : GOAction
    {

        [InParam("target")]
        public GameObject target;



        public override void OnStart()
        {
            base.OnStart();
        } // OnStart


        //public override TaskStatus OnUpdate()
        //{
        //    //if (shooter == null)
        //    //{
        //    //    return TaskStatus.FAILED;
        //    //}

        //    //return shooter.Shoot(target != null ? target.transform : null) ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        //} // OnUpdate

    } // class DoneShootOnce

} // namespace