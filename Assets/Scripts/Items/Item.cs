using System;
using System.Collections;
using System.Threading.Tasks;
using Common.MonoBehaviour;
using Project;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Item : CashedMonoBehaviour
    {
        [SerializeField] private AudioClip _pickUpClip;
        
        private CircleCollider2D _collider;
        
        protected override void InheritStart()
        {
            base.InheritStart();

            _collider = GetComponent<CircleCollider2D>();
        }

        protected virtual void PickUp(Character.Character character)
        {
            ProjectContext.Instance.SoundPlayer.PlayClip(_pickUpClip);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                PickUp(character);
                Destroy(gameObject);
            }
        }
    }
}