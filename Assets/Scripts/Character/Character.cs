using Common.MonoBehaviour;
using Common.Entities;
using Project;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Character
{
    [RequireComponent(typeof(Movement.Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Character : CashedMonoBehaviour
    {
        public event UnityAction<Character> Death;
        
        [SerializeField] private EnemyScaner _enemyScaner;
        [SerializeField] private AudioClip _takeDamageClip;
        
        private Health _health;
        private Movement.Movement _movement;
        private CapsuleCollider2D _collider;
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        public Health Health => _health;
        public Rigidbody2D Rigidbody => _rigidbody;
        public EEntityState EntityState { get; private set; }

        protected override void InheritStart()
        {
            base.InheritStart();

            _health = GetComponent<Health>();
            _movement = GetComponent<Movement.Movement>();
            _collider = GetComponent<CapsuleCollider2D>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            EntityState = EEntityState.Alive;
            _health.Death += Die;
            _health.TakingDamage += TakeDamage;
        }

        private void Die()
        {
            _animator.Play(AnimationNamesContainer.DEATH);
            _movement.enabled = false;
            _collider.enabled = false;

            EntityState = EEntityState.Death;
            Death?.Invoke(this);
        }

        private void TakeDamage(int health)
        {
            ProjectContext.Instance.SoundPlayer.PlayClip(_takeDamageClip);
        }

        private void Update()
        {
            if (EntityState == EEntityState.Death)
                return;
            
            Vector2 origin = transform.position;
            RaycastHit2D hit =
                Physics2D.Raycast(_enemyScaner.transform.position, Vector2.down, _enemyScaner.RayDistance, _enemyScaner.EnemyMask);
            
            if (hit)
            {
                if (hit.collider.TryGetComponent(out IDamageble enemy))
                {
                    _movement.Jump();
                    enemy.TakeDamage();
                }
            }
        }
    }
}