using Common.Entities;
using Common.MonoBehaviour;
using UnityEngine;

namespace UI.Battle.HealthPresenter
{
    public class HealthPanel : CashedMonoBehaviour
    {
        [SerializeField] private Health _character;
        [SerializeField] private Heart[] _hearts;

        private void AddHealth(int health)
        {
            if (health <= 0 || health > 2) 
                return;

            int remainingHealth = health;
            foreach (Heart heart in _hearts)
            {
                if (remainingHealth <= 0)
                    return;

                if (heart.CurrentHealth < heart.MAX_HEALTH)
                {
                    int h = heart.MAX_HEALTH - heart.CurrentHealth;
                    if (health <= h)
                    {
                        heart.AddHealth(health);   
                        return;
                    }
                    
                    heart.AddHealth(h);
                    remainingHealth = health - h;
                }
            }
        }

        private void RemoveHealth(int health)
        {
            if (health <= 0)
                return;
            
            int remainingHealth = health;
            for (int i = _hearts.Length - 1; i >= 0; i--)
            {
                Heart heart = _hearts[i];
                
                if (remainingHealth <= 0)
                    return;

                if (heart.CurrentHealth > heart.MIM_HEALTH)
                {
                    int h = heart.MIM_HEALTH + heart.CurrentHealth;
                    if (health <= h)
                    {
                        heart.RemoveHealth(health);   
                        return;
                    }
                    
                    heart.RemoveHealth(h);
                    remainingHealth = health - h;
                }
            }
        }

        protected override void InheritStart()
        {
            base.InheritStart();

            _character.TakingDamage += RemoveHealth;
            _character.Healing += AddHealth;
            
            AddHealth(_character.CurrentHealth);
        }
    }
}