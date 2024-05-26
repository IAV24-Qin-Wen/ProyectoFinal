using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using HelpURL = BehaviorDesigner.Runtime.Tasks.HelpURLAttribute;
using Unity.VisualScripting;

namespace BehaviorDesigner.Runtime.Tactical.Tasks
{
    [TaskCategory("Tactical")]
    [TaskDescription("Moves to the closest target and starts attacking as soon as the agent is within distance")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-tactical-pack/")]
    [TaskIcon("Assets/Behavior Designer Tactical/Editor/Icons/{SkinColor}AttackIcon.png")]


    public class Attack : NavMeshTacticalGroup
    {
        [UnityEngine.Serialization.FormerlySerializedAs("attacked")]
        public SharedBool attacked;
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject target;
        bool triedAttacked;
        private AttackArea attackArea;
        public override void OnAwake()
        {
            attackArea=gameObject.GetComponent<AttackArea>();
            triedAttacked = false;
        }


        public override TaskStatus OnUpdate()
        {
            if (!triedAttacked) { attackArea.TryAttack(); triedAttacked = true; return TaskStatus.Running; }
            else if (attackArea.IsAttacking()) 
            {
                return TaskStatus.Running; }


            else { triedAttacked = false; return TaskStatus.Success; }
     
        }
    }
}