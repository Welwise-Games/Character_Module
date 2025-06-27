using UnityEngine;
using WelwiseSharedModule.Runtime.Shared.Scripts;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.HeroAnimators
{
    public class ArmsAnimatorController
    {
        private readonly ArmsAnimatorSerializableComponents _armsAnimatorSerializableComponents;
        private readonly int _isRunningHash = Animator.StringToHash("isRunning");
        private readonly int _jumpHash = Animator.StringToHash("jump");
        private readonly int _idleHash = Animator.StringToHash("idle");

        private readonly Timer _timer;
        private readonly Animator _animator;

        public ArmsAnimatorController(ArmsAnimatorSerializableComponents armsAnimatorSerializableComponents, Animator animator)
        {
            _armsAnimatorSerializableComponents = armsAnimatorSerializableComponents;
            _animator = animator;
            _timer = new Timer(armsAnimatorSerializableComponents.destroyCancellationToken);

            _timer.Ended += () => _animator.SetTrigger(_idleHash);
        }
        
        public void TriggerJump() => _animator.SetTrigger(_jumpHash);

        public void SetIsRunning(bool isRunning)
        {
            if (isRunning)
            {
                _timer.TryStoppingCountingTime();
                _timer.TryStartingCountingTime(_armsAnimatorSerializableComponents.IdleAnimationDelay);
            }
            
            _animator.SetBool(_isRunningHash, isRunning);
        }
    }
}