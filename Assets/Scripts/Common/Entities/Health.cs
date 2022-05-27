using System.Collections;
using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Entities
{
    public class Health : CashedMonoBehaviour, IDamageble
    {
        public event UnityAction Death;
        public event UnityAction<int> TakingDamage;
        public event UnityAction<int> Healing; 

        [SerializeField] private int _health;
        private bool _immortality;

        private const int DAMAGE = 1;
        private const float IMMORTALITY_TIME = 0.5f;

        public int CurrentHealth => _health;
        
        public void Cure(int health)
        {
            _health += health;
            Healing?.Invoke(health);
        }
        
        public void TakeDamage()
        {
            if (_immortality)
                return;
            
            _health -= DAMAGE;
            TakingDamage?.Invoke(1);
            StartCoroutine(Immortality());
            
            if (_health <= 0) 
                Die();
        }
        
        protected override void InheritStart()
        {
            base.InheritStart();
            
            _immortality = false;
        }

        private void Die()
        {
            Death?.Invoke();
        }

        private IEnumerator Immortality()
        {
            _immortality = true;
            yield return new WaitForSecondsRealtime(IMMORTALITY_TIME);
            _immortality = false;
        }
    }
}