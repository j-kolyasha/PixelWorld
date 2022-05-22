using System.Collections;
using System.Threading.Tasks;
using Common.Entities;
using Enemys.BaseEnemy;
using Environment;
using UnityEngine;

namespace Enemys
{
    public class IronBall : Enemy<IronBall>
    {
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _activateTime;
        [SerializeField] private LayerMask _playerLayer;

        [Header("Effect Settings")]
        [SerializeField, Range(0, 1000)] private int _effectTime;
        [SerializeField] private SpriteRenderer _ballSprite;
        [SerializeField] private GameObject _explosionEffect;

        private Coroutine _timer;

        private async void Explosion(IDamageble player)
        {
            _ballSprite.enabled = false;
         
            ProjectContext.Instance.MainCamera.Explosion();
            AudioSource.Play();
            _explosionEffect.gameObject.SetActive(true);
            
            player.TakeDamage();

            await Task.Delay(_effectTime);

            Health.TakeDamage();
        }
        private void Update()
        {
            Collider2D collision = Physics2D.OverlapCircle(transform.position, _attackRadius, _playerLayer);
            if (collision)
            {
                if (_timer != null)
                    return;
                
                _timer = StartCoroutine(StartTimer(collision.GetComponent<IDamageble>()));
            }
            else if (_timer != null)
            {
                StopCoroutine(_timer);
                _ballSprite.color = Color.white;
                _timer = null;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageble player))
            {
                Explosion(player);
            }
        }

        private IEnumerator StartTimer(IDamageble player)
        {
            float time = _activateTime;
            while (time >= 0)
            {
                _ballSprite.color = Color.Lerp(_ballSprite.color, Color.red, _activateTime * Time.deltaTime);
                time -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            Explosion(player);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
    }
}