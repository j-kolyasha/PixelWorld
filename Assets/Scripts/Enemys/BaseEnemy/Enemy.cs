using System;
using System.Collections;
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
    [RequireComponent(typeof(Collider2D))]
    public abstract class Enemy<T> : CashedMonoBehaviour where T : Enemy<T>
    {
        public event UnityAction<T> Death;

        [SerializeField] private AudioClip _deathClip;

        protected EEntityState EntityState { get; private set; }
        protected Health Health { get; private set; }
        protected Animator Animator { get; private set; }
        protected Rigidbody2D Rigidbody { get; private set; }
        protected AudioSource Audio { get; private set; }
        protected Collider2D SelfCollider { get; private set; }
        protected SpriteRenderer SpriteRenderer { get; private set; }
        

        protected override void InheritStart()
        {
            base.InheritStart();

            Health = GetComponent<Health>();
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Audio = GetComponent<AudioSource>();
            SelfCollider = GetComponent<Collider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            EntityState = EEntityState.Alive;
            Health.Death += Die;
        }

        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();

            Health.Death -= Die;
        }
        

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IDamageble player))
            {
                player.TakeDamage();
            }
        }
        protected virtual void Die()
        {
            EntityState = EEntityState.Death;
            Death?.Invoke(this as T);

            StartCoroutine(Destroyed());
        }

        protected virtual IEnumerator Destroyed()
        {
            Audio.clip = _deathClip;
            Audio.Play();
            SelfCollider.enabled = false; 
            
            yield return new WaitForSecondsRealtime(3f);
            Destroy(gameObject);
        }
    }
}