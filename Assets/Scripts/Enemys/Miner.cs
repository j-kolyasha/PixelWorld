using Common;
using Enemys.BaseEnemy;
using Enemys.Movement;
using UnityEngine;

namespace Enemys
{
    public class Miner : MovementEnemy<Miner>
    {
        [SerializeField, Range(0f, 5f)] private float _restTime;
        [SerializeField] private EnemyMovePoint[] _movePoints;
        
        private int CountMovePoints => _movePoints.Length;
        
        protected override void InheritStart()
        {
            base.InheritStart();

            EndMove += Rest;
        }

        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();

            EndMove -= Rest;
        }

        private async void Rest()
        {
            await Timer.Await(_restTime);
            SelectNextMovePoint();
        }

        protected override void SelectNextMovePoint()
        {
            int pointId = Random.Range(0, CountMovePoints);
            MoveTo(_movePoints[pointId]);
        }
    }
}