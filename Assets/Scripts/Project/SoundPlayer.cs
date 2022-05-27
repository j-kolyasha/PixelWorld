using System.Collections.Generic;
using Common.MonoBehaviour;
using UnityEngine;

namespace Project
{
    public class SoundPlayer : CashedMonoBehaviour
    {
        private List<AudioSource> _audioSources;

        private const string PREFIX = "AudioSource";
        private const float VOLUME = 0.4f;
        
        protected override void InheritAwake()
        {
            base.InheritAwake();
            
            _audioSources = new List<AudioSource>();
        }

        public void PlayClip(AudioClip clip)
        {
            foreach (AudioSource audio in _audioSources)
            {
                if (audio.isPlaying == false)
                {
                    Play(audio, clip);
                    return;
                }
            }

            AudioSource audioSource = CreateNewAudioSource(clip.name);
            Play(audioSource, clip);
            
            _audioSources.Add(audioSource);
        }

        private void Play(AudioSource audioSource, AudioClip clip)
        {
            audioSource.transform.name = $"{PREFIX} - {clip.name}";

            audioSource.clip = clip;
            audioSource.Play();
        }

        private AudioSource CreateNewAudioSource(string clipName)
        {
            GameObject audioSourceObject = new GameObject();
            audioSourceObject.transform.parent = transform;
            
            AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.volume = VOLUME;

            return audioSource;
        }
    }
}