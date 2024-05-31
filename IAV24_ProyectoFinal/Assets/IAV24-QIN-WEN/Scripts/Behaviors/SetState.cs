using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{

    [TaskDescription("Set state of the character on UI")]
    [TaskCategory("IAV24")]
    [TaskIcon("{SkinColor}RepeaterIcon.png")]

    public class SetState : Action
    {
        [UnityEngine.Serialization.FormerlySerializedAs("textGO")]
        public SharedGameObject textGO;
        private TextMeshProUGUI textMeshPro;

        [UnityEngine.Serialization.FormerlySerializedAs("state")]
        public string state;
        public override void OnStart()
        {
            textMeshPro = textGO.Value.GetComponent<TextMeshProUGUI>();
        }

        public override TaskStatus OnUpdate()
        {
            textMeshPro.text = state;

            return TaskStatus.Success;
        }


    }
}