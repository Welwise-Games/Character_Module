using FishNet.Component.Animating;
using UnityEngine;

namespace WelwiseCharacterModule.Runtime.Shared.Scripts
{
    public class HeroAnimatorController
    {
        private readonly int _isRunningToHash = Animator.StringToHash("isRunning");
        private readonly int _isFallingToHash = Animator.StringToHash("isFalling");
        private readonly int _jumpToHash = Animator.StringToHash("jump");
        private readonly Animator _animator;
        private readonly NetworkAnimator _networkAnimator;

        public HeroAnimatorController(HeroAnimatorSerializableComponents animatorSerializableComponents)
        {
            _animator = animatorSerializableComponents.Animator;
            _networkAnimator = animatorSerializableComponents.NetworkAnimator;
        }

        public HeroAnimatorController(Animator animator, NetworkAnimator networkAnimator)
        {
            _animator = animator;
            _networkAnimator = networkAnimator;
        }

        public void TriggerJump() => _networkAnimator.SetTrigger(_jumpToHash);

        public void SetIsRunning(bool isRunning) =>
            _animator.SetBool(_isRunningToHash, isRunning);

        public void SetIsFalling(bool isFalling) =>
            _animator.SetBool(_isFallingToHash, isFalling);
    }
}