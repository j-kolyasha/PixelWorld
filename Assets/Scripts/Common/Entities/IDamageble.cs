using UnityEngine.Events;

namespace Common.Entities
{
    public interface IDamageble
    {
        public event UnityAction Death;
        public event UnityAction<int> Damage;

        public void TakeDamage();
    }
}