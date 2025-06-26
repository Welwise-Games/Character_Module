using UnityEngine;
using WelwiseCharacterModule.Runtime.Client.Scripts.OwnerPlayerMovement;
using WelwiseSharedModule.Runtime.Client.Scripts;
using WelwiseSharedModule.Runtime.Client.Scripts.Tools;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerCamera
{
    public class OwnerPlayerAudioController
    {
        public OwnerPlayerAudioController(OwnerPlayerMovementController movementController, AudioSource walkingAudioSource, AudioSource jumpingAudioSource,
            HeroAudioClipsProviderService heroAudioClipsProviderService)
        {
            movementController.MovedOnGround += PlayWalkingSoundAsync;
            movementController.Jumped += PlayJumpSoundAsync;
            movementController.NotMovedOnGround += StopWalkingSound;


            async void PlayWalkingSoundAsync()
            {
                var clip = (await heroAudioClipsProviderService.GetHeroAudioClipsConfigAsync()).WalkingOnGroundClip;
                walkingAudioSource.SetPitchAndPlay(clip);  
            }

            void StopWalkingSound()
            {
                if (walkingAudioSource.isPlaying)
                    walkingAudioSource.Stop();
            }

            async void PlayJumpSoundAsync()
            {
                jumpingAudioSource.SetPitchAndPlayOneShot(
                    (await heroAudioClipsProviderService.GetHeroAudioClipsConfigAsync()).JumpClip);
            }
        }
    }
}