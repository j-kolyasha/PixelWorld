using System.Collections;
using Enemys.BaseEnemy;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Bad : MovementEnemy<Bad>
    {
        protected override IEnumerator Destroyed()
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;

            return base.Destroyed();
        }
    }
}