using System.Collections;
using Enemys.BaseEnemy;
using Enemys.Movement;
using UnityEngine;

namespace Enemys
{
    public class Bad : MovementEnemy<Bad>
    {
        [SerializeField] private EnemyMovePoint[] _points;
        private int _nextMovePoint = 0;
        
        protected override void InheritStart()
        {
            base.InheritStart();

            EndMove += SelectNextMovePoint;
            SelectNextMovePoint();
        }

        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();

            EndMove -= SelectNextMovePoint;
        }

        protected override void SelectNextMovePoint()
        {
            if (_nextMovePoint >= _points.Length)
                _nextMovePoint = 0;
            
            MoveTo(_points[_nextMovePoint]);
            _nextMovePoint++;
        }
    }
}