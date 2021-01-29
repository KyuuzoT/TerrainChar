using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Environment
{
    public class GlobalSounding : MonoBehaviour
    {
        [SerializeField] private EnvironmentSounds sounds;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            sounds.Init(audioSource);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Trees"))
            {
                sounds.PlayForestSound();
            }
            if(other.tag.Equals("Waters"))
            {
                sounds.PlayWaterSound();
            }
            if(other.tag.Equals("Mountain"))
            {
                sounds.PlayMountainSound();
            }
            if(other.tag.Equals("Stairs"))
            {
                sounds.PlayStairsSound();
            }
        }
    }
}
