using Common.MonoBehaviour;
using UnityEngine;

namespace Character
{
    public class EnemyScaner : CashedMonoBehaviour
    {
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private float _rayDistance;
        
        public LayerMask EnemyMask => _enemyMask;
        public float RayDistance => _rayDistance;
    }
}