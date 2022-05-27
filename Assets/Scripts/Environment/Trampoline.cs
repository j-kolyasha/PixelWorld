using Common.MonoBehaviour;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Trampoline : CashedMonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _jumpForce;
    
    private Animator _animator;
    private AudioSource _audioSource;

    private const float JUMP_FORCE_MODIFIRE = 10f; 

    protected override void InheritStart()
    {
        base.InheritStart();

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Character.Character character))
        {
            _audioSource.Play();
            _animator.Play("Jump");
            
            character.Rigidbody.AddForce(Vector2.up * _jumpForce * JUMP_FORCE_MODIFIRE, ForceMode2D.Impulse);
        }
    }
}
