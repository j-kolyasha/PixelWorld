using Common.MonoBehaviour;
using UnityEngine;

namespace Environment
{
    public class MainCamera : CashedMonoBehaviour
    {
        private Animator _animator;

        private const string EXPLOSION_ANIMATION = "Explosion";

        protected override void InheritStart()
        {
            base.InheritStart();

            _animator = GetComponent<Animator>();
        }

        public void Explosion()
        {
            _animator.Play(EXPLOSION_ANIMATION);
        }
    }
}