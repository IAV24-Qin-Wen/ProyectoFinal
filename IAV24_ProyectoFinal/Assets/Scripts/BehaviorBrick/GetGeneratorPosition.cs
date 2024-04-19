using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("IAV24/GetGeneratorPosition")]
    public class GetGeneratorPosition : GOAction
    {
        [InParam("Map")]
        public MapInfo map;

        [InParam("ID")]
        public int ID;

        [OutParam("position")]
        public Vector3 position;


        public override void OnStart()
        {

        }


        public override TaskStatus OnUpdate()
        {
            position = map.GetGenerators()[ID].go.transform.position;
            return TaskStatus.COMPLETED;

        }

    }
}
