using LiquidSnake.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace LiquidSnake.LevelObjects
{
    public class ButtonAreaTrigger : MonoBehaviour, IResetteable
    {
        [SerializeField]
        private UnityEvent onAreaEntered;
        [SerializeField]
        private Material material1, material2;


        [SerializeField]
        private GameObject lightColor;
        private Renderer render = null;

        private bool actMat = true;

        public void Start()
        {
            render = lightColor.GetComponent<Renderer>();
        }

        public void Reset()
        {
            actMat = true;
            if(render != null) 
            render.material = material1;
 
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<SwitchControlMode>().IsAutoMode() && Input.GetMouseButtonUp(1))
            {
                render.material = ((actMat = !actMat) ? material1 : material2);
                Debug.Log(actMat);
                onAreaEntered?.Invoke();
            }
        }

        public void toggle() { 
            render.material = ((actMat = !actMat) ? material1 : material2); 
            onAreaEntered?.Invoke(); 
        }
    }

} // namespace LiquidSnake.LevelObjects
