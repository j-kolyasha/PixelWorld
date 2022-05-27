using System.Collections;
using Enemys.BaseEnemy;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Bag : MovementEnemy<Bag>
    {
        [Header("SettingsNewAppearance")]
        [SerializeField] private Animator _rebirthEffect;
        [SerializeField] private Sprite _smallBag;
        [SerializeField] private AudioClip _rebornAudio;

        private BoxCollider2D _collider;
        
        private const string REBORN_ANIMATION = "Reborn";
        private const float SMALL_BAG_COLLIDER_SIZE = 0.6f;

        protected override void InheritStart()
        {
            base.InheritStart();

            _collider = GetComponent<BoxCollider2D>();
            Health.TakingDamage += ChangeAppearance;
        }
        
        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();

            Health.TakingDamage += ChangeAppearance;
        }

        private void ChangeAppearance(int health)
        {
            _rebirthEffect.gameObject.SetActive(true);
            _rebirthEffect.Play(REBORN_ANIMATION);
            
            Audio.clip = _rebornAudio;
            Audio.Play();

            _collider.size = new Vector2(SMALL_BAG_COLLIDER_SIZE, SMALL_BAG_COLLIDER_SIZE);
            SpriteRenderer.sprite = _smallBag;

            StartCoroutine(DisableAnimations());
        }

        private IEnumerator DisableAnimations()
        {
            yield return new WaitForSecondsRealtime(0.3f);
            _rebirthEffect.gameObject.SetActive(false);
        }
    }
}