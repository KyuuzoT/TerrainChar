using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(AudioSource))]
[Serializable]
public class PlayerSounds
{
    [Header("Sounds")]
    [SerializeField] private Sounds sounds;
    [SerializeField] private AudioSource audioSrc;
    private CharacterController controller;

    internal void Init(CharacterController controller) => InternalInit(controller);
    internal void PlayLandingSound() => InternalPlayLandingSound();
    internal void PlayJumpingSound() => InternalPlayJumpingSound();
    internal void PlayWalkingSound() => InternalPlayWalkingSounds();
    internal void PlaySprintingSound() => InternalPlaySprintingSounds();
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
        if (!controller.isGrounded)
        {
            return;
        }

        audioSrc.clip = sounds.jumpSound;
        audioSrc.Play();
    }

    private void InternalPlayWalkingSounds()
    {
        if(!controller.isGrounded)
        {
            return;
        }
        Random rnd = new Random();
        int i = rnd.Next(1, sounds.walkingSounds.Length);
        audioSrc.clip = sounds.walkingSounds[i];
        audioSrc.PlayOneShot(audioSrc.clip);
        sounds.walkingSounds[i] = sounds.walkingSounds[0];
        sounds.walkingSounds[0] = audioSrc.clip;
    }

    private void InternalPlaySprintingSounds()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        audioSrc.clip = sounds.landingSound;
        audioSrc.Play();
    }

    [SerializeField]
    private struct Sounds
    {
        [SerializeField] internal AudioClip jumpSound;
        [SerializeField] internal AudioClip landingSound;
        [SerializeField] internal AudioClip[] walkingSounds;
        [SerializeField] internal AudioClip[] sprintSounds;
    }
}
