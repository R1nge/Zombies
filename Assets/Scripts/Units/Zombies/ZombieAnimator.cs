using UnityEngine;

namespace Units.Zombies
{
    public class ZombieAnimator
    {
        private readonly Animator _animator;
        private static readonly int Bite = Animator.StringToHash("Bite");

        public ZombieAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayBiteAnimation()
        {
            _animator.SetTrigger(Bite);
        }
    }
}