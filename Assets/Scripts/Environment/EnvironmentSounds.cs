using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Environment
{
    [Serializable]
    public class EnvironmentSounds
    {
        [SerializeField] private Sounds natureSounds;
        [SerializeField] private AudioSource audioSrc;


        internal void Init(AudioSource source) => Init(source);
        internal void PlayForestSound() => InternalPlayForestSound();
        internal void PlayWaterSound() => InternalPlayWaterSound();
        internal void PlayMountainSound() => InternalPlayMountainSound();
        internal void PlayStairsSound() => InternalPlayStairsSound();


        private void InternalInit(AudioSource source)
        {
            audioSrc = source;
        }

        private void InternalPlayForestSound()
        {
            audioSrc.clip = natureSounds.forestSounds;
            audioSrc.loop = true;
            audioSrc.Play();
        }

        private void InternalPlayWaterSound()
        {
            audioSrc.clip = natureSounds.waterSounds;
            audioSrc.loop = true;
            audioSrc.Play();
        }

        private void InternalPlayMountainSound()
        {
            audioSrc.clip = natureSounds.mountainSounds;
            audioSrc.loop = true;
            audioSrc.Play();
        }

        private void InternalPlayStairsSound()
        {
            audioSrc.clip = natureSounds.stairsSounds;
            audioSrc.loop = true;
            audioSrc.Play();
        }

        [Serializable]
        private struct Sounds
        {
            [SerializeField] internal AudioClip forestSounds;
            [SerializeField] internal AudioClip waterSounds;
            [SerializeField] internal AudioClip mountainSounds;
            [SerializeField] internal AudioClip stairsSounds;
        }
    }
}

