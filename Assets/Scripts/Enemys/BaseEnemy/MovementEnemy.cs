using System.Collections;
using Enemys.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace Enemys.BaseEnemy
{
    public class MovementEnemy<T> : Enemy<T> where T : MovementEnemy<T>
    {
        public event UnityAction EndMove;

        [SerializeField, Range(0.1f, 5f)] private float _speed;
        private Coroutine _movement;
        
        protected void MoveTo(EnemyMovePoint movePoint)
        {
            if(_movement != null)
                StopCoroutine(_movement);
                
            _movement = StartCoroutine(Move(movePoint));
        }

        protected virtual IEnumerator Move(EnemyMovePoint point)
       {
           Vector3 selfPosition;
           Vector3 pointPosition;
           do
           {
               selfPosition = transform.localPosition;
               pointPosition = point.transform.localPosition;
                
               transform.localPosition = Vector3.MoveTowards(selfPosition, pointPosition, _speed * Time.deltaTime);
               yield return new WaitForEndOfFrame();
           } while (Vector3.Distance(selfPosition, pointPosition) > 0.1f);
           
           EndMove?.Invoke();
       }
    }
}