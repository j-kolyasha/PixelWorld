using System;
using System.Collections;
using Common.Entities;
using Common.MonoBehaviour;
using Project;
using UnityEngine;

namespace Character.Movement
{
    public class Movement : CashedMonoBehaviour
    {
        [Header(("Ground Scaners"))]
        [SerializeField] private GroundScaner _rightPoint;
        [SerializeField] private GroundScaner _leftPoint;
        
        [Header("Settings")]
        [SerializeField, Range(1f, 10f)] private float _speedMovement;
        [SerializeField, Range(1f, 20f)] private float _jumpForce;
        [SerializeField] private AudioClip _jumpClip;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private EMovementState _movementState;
        
        public void Jump()
        {
            Vector2 direction = _rigidbody.velocity;
            direction.y = _jumpForce;
            _rigidbody.velocity = direction;
            
            _movementState = EMovementState.Fall;
            ChangeAnimation();
            StartCoroutine(WaitingGround());
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _movementState = EMovementState.Idle;
            PlayerInput.Instance.SpaceClick += CheckingPossibilityJump;
        }

        private void OnDestroy()
        {
            PlayerInput.Instance.SpaceClick -= CheckingPossibilityJump;
        }

        private void Update()
        {
            Vector2 direction = new Vector2(PlayerInput.Instance.HorizontalAxis * _speedMovement,
                _rigidbody.velocity.y);

            if (CheckGround())
            {
                if (Math.Abs(direction.x) > 0.05f)
                    _movementState = EMovementState.Walk;
                else
                    _movementState = EMovementState.Idle;

                ChangeRotation();
                ChangeAnimation();
            }
            else
            {
                direction.y -= Math.Abs(Physics2D.gravity.y * Time.deltaTime);
            }

            _rigidbody.velocity = direction;
        }

        private void CheckingPossibilityJump()
        {
            if (CheckGround())
            {
                ProjectContext.Instance.SoundPlayer.PlayClip(_jumpClip);
                Jump();
            }
        }
        
        private IEnumerator WaitingGround()
        {
            while (CheckGround() == false)
            {
                yield return null;
            }

            _movementState = EMovementState.Idle;
            ChangeAnimation();
        }

        private bool CheckGround()
        {
            if (_rightPoint.GetGroundStatus() || _leftPoint.GetGroundStatus())
                return true;

            return false;
        }

        private void ChangeAnimation()
        {
            switch (_movementState)
            {
                case EMovementState.Idle:
                    _animator.Play(AnimationNamesContainer.IDLE);
                    break;
                case EMovementState.Walk:
                    _animator.Play(AnimationNamesContainer.WALK);
                    break;
                case EMovementState.Fall:
                    _animator.Play(AnimationNamesContainer.FALL);
                    break;
            }
        }

        private void ChangeRotation()
        {
            if (_rigidbody.velocity.x > 0.05f)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (_rigidbody.velocity.x < -0.05f)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
