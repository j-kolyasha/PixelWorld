using Common.MonoBehaviour;
using UnityEngine;

namespace Character.Movement
{
    public class GroundScaner : CashedMonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        
        private const float RAY_DISTANCE = 0.2f;
        
        public bool GetGroundStatus()
        {
            Vector2 origin = transform.position;
            if (Physics2D.Raycast(origin, Vector2.down, RAY_DISTANCE, _groundLayer))
                return true;

            return false;
        }
    }
}