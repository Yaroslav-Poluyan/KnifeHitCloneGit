using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Current;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource melonSound;
        [SerializeField] private AudioSource clickMenuSound;
        [SerializeField] private AudioSource intercectionSound;
        [SerializeField] private AudioSource woodLogDestructionSound;
        [SerializeField] private AudioSource woodAppearingSound;
        [SerializeField] private AudioSource stuckSound;

        public void PlayKnifeStuck()
        {
            stuckSound.Play();
        }
        public void PlayWoodDestruction()
        {
            woodLogDestructionSound.Play();
        }
        public void PlayWoodAppearing()
        {
            woodAppearingSound.Play();
        }

        public void PlayClickMenu()
        {
            clickMenuSound.Play();
        }

        public void PlayIntercectionSound()
        {
            intercectionSound.Play();
        }

        public void PlayWaterMelonSound()
        {
            melonSound.Play();
        }
        private void Awake()
        {
            Current = this;
        }
    }
}