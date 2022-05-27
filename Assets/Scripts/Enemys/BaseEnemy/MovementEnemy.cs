using System.Collections;
using Common.Entities;
using Enemys.Movement;
using UnityEngine;

namespace Enemys.BaseEnemy
{
    public class MovementEnemy<T> : Enemy<T> where T : MovementEnemy<T>
    {

        [SerializeField, Range(0.1f, 5f)] private float _speed;
        [SerializeField] private EnemyMovePoint[] _points;
        private int _nextMovePoint = 0;
        private Coroutine _movement;

        protected override void InheritStart()
        {
            base.InheritStart();
            
            SelectNextMovePoint();
        }

        protected virtual void SelectNextMovePoint()
        {
            if (_nextMovePoint >= _points.Length)
                _nextMovePoint = 0;
            
            MoveTo(_points[_nextMovePoint]);
            _nextMovePoint++;
        }

        protected void StopMovement()
        {
            if (_movement != null)
                StopCoroutine(_movement);
        }
        
        protected void MoveTo(EnemyMovePoint movePoint)
        {
            if (EntityState == EEntityState.Death)
                return;
            
            if(_movement != null)
                StopCoroutine(_movement);
                
            ChangeRotation(movePoint.transform.localPosition);
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
           } while (Vector3.Distance(selfPosition, pointPosition) > 0.3f);
           
           SelectNextMovePoint();
       }

        private void ChangeRotation(Vector2 pointPosition)
        {
            float x = transform.localPosition.x;
            
            if (pointPosition.x > x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            if (pointPosition.x < x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}