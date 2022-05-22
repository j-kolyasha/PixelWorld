using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Entities
{
    public class PlayerInput : GenericSingleton<PlayerInput>
    {
        public event UnityAction SpaceClick;
        public float HorizontalAxis { get; private set; }

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const KeyCode SPACE_CODE = KeyCode.Space; 
        
        private void Update()
        {
            HorizontalAxis = Input.GetAxis(HORIZONTAL_AXIS);

            if (Input.GetKeyDown(SPACE_CODE))
                SpaceClick?.Invoke();
        }
    }
}