using System;
using Common.Entities;
using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.Events;

namespace Enemys.BaseEnemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class Enemy<T> : CashedMonoBehaviour where T : Enemy<T>
    {
        public event UnityAction<T> Death;

        protected Health Health { get; private set; }
        protected Animator Animator { get; private set; }
        protected Rigidbody2D Rigidbody { get; private set; }
        protected AudioSource AudioSource { get; private set; }
        

        protected override void InheritStart()
        {
            base.InheritStart();

            Health = GetComponent<Health>();
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            AudioSource = GetComponent<AudioSource>();
            
            Health.Death += Die;
        }

        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();

            Health.Death -= Die;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageble player))
            {
                player.TakeDamage();
            }
        }

        protected virtual void SelectNextMovePoint()
        {
            
        }

        private void Die()
        {
            Death?.Invoke(this as T);
            Debug.Log("Destroy Enemy");
            Destroy(gameObject);
        }
    }
}