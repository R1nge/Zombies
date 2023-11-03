using UnityEngine;

namespace Units
{
    public class UnitAnimator
    {
        private readonly Animator _animator;
        private static readonly int Bite = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("IsDead");
        private static readonly int StandUp = Animator.StringToHash("StandUp");
        
        public UnitAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(Bite);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger(Death);
        }

        public void PlayStandUpAnimation()
        {
            _animator.SetTrigger(StandUp);
        }
    }
}