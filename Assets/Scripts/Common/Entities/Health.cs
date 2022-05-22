using System.Collections;
using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Entities
{
    public class Health : CashedMonoBehaviour, IDamageble
    {
        public event UnityAction Death;
        public event UnityAction<int> Damage;

        [SerializeField] private int _health;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private const int DAMAGE_UNIT = 1;

        protected override void InheritStart()
        {
            base.InheritStart();

            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage()
        {
            _health -= DAMAGE_UNIT;
            Damage?.Invoke(_health);
            
            if (_health <= 0) 
                Die();
        }

        private void Die()
        {
            Death?.Invoke();
        }
    }
}