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
            Debug.Log(other);
        }
    }
}
