using System;
using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle.HealthPresenter
{
    public class Heart : CashedMonoBehaviour
    {
        [SerializeField] private HealthSpriteContainer _healthSprites;

        private Image _image;
        private int _currentHealth = 0;

        public int MAX_HEALTH = 2;
        public int MIM_HEALTH = 0;
        
        public int CurrentHealth => _currentHealth;
        
        public void AddHealth(int health)
        {
            if (health <= 0)
                return;
            
            if (_currentHealth + health > MAX_HEALTH)
                return;

            _currentHealth += health;
            ChangeSprite();
        }

        public void RemoveHealth(int health)
        {
            if (health <= 0)
                return;

            if (_currentHealth - health < MIM_HEALTH)
                return;

            _currentHealth -= health;
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            switch (_currentHealth)
            {
                case 0:
                    _image.sprite = _healthSprites.EmptyHeart;
                    break;
                case 1:
                    _image.sprite = _healthSprites.HalfHeart;
                    break;
                case 2:
                    _image.sprite = _healthSprites.FullHeart;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void InheritAwake()
        {
            base.InheritAwake();
            
            _image = GetComponent<Image>();
        }
    }
}