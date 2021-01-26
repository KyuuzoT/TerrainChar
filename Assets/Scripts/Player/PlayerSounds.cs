using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private struct Sounds
    {
        internal AudioClip jumpSound;
        internal AudioClip landingSound;
        internal AudioClip[] walkingSounds;
        internal AudioClip[] sprintSounds;
    }

    [SerializeField] private Sounds sounds;
    private AudioSource audioSrc;
    private CharacterController controller;

    private void InternalInit(CharacterController controller)
    {
        this.controller = controller;
    }

    private void InternalPlayLandingSound()
    {
        audioSrc.clip = sounds.landingSound;
        audioSrc.Play();
    }

    private void InternalPlayJumpingSound()
    {
        audioSrc.clip = sounds.jumpSound;
        audioSrc.Play();
    }

    private void InternalPlayWalkingSounds()
    {
        if(!controller.isGrounded)
        {
            return;
        }

        audioSrc.clip = sounds.landingSound;
        audioSrc.Play();
    }

    private void InternalPlaySprintSounds()
    {
        audioSrc.clip = sounds.landingSound;
        audioSrc.Play();
    }
}
