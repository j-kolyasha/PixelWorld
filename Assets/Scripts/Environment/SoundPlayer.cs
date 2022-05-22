using Common.MonoBehaviour;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : CashedMonoBehaviour
    {
        private AudioSource _audioSource;
        
        public void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        protected override void InheritStart()
        {
            base.InheritStart();

            _audioSource = GetComponent<AudioSource>();
        }
    }
}