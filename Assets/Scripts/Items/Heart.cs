
using UnityEngine;

namespace Items
{
    public class Heart : Item
    {
        [SerializeField, Range(1, 2)] private int _health;
        
        protected override void PickUp(Character.Character character)
        {
            base.PickUp(character);
            
            character.Health.Cure(_health);
        }
    }
}