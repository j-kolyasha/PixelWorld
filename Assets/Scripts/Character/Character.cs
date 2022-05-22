using Common.MonoBehaviour;
using Common.Entities;
using UnityEngine;
using UnityEngine.Events;

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
        private Health _health;
        private Movement.Movement _movement;
        private CapsuleCollider2D _collider;
        private Animator _animator;
        
        public ECharacterState CharacterState { get; private set; }

        protected override void InheritStart()
        {
            base.InheritStart();

            _health = GetComponent<Health>();
            _movement = GetComponent<Movement.Movement>();
            _collider = GetComponent<CapsuleCollider2D>();
            _animator = GetComponent<Animator>();

            CharacterState = ECharacterState.Alive;
            _health.Death += Die;
        }

        private void Die()
        {
            _animator.Play(AnimationNamesContainer.DEATH);
            _movement.enabled = false;
            _collider.enabled = false;

            CharacterState = ECharacterState.Death;
            Death?.Invoke(this);
        }
        
        private void Update()
        {
            if (CharacterState == ECharacterState.Death)
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