using Units.Humans;
using UnityEngine;

namespace Units.Zombies
{
    public class ZombieUnit : Unit
    {
        private ZombieUnitStateMachine _zombieUnitStateMachine;
        private ZombieAnimator _zombieAnimator;

        protected override void Awake()
        {
            base.Awake();
            _zombieAnimator = new ZombieAnimator(animator);
            _zombieUnitStateMachine = new ZombieUnitStateMachine(UnitMovement);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.transform.TryGetComponent(out HumanUnit humanUnit))
            {
                _zombieAnimator.PlayBiteAnimation();
            }
        }

        protected override void Update() => _zombieUnitStateMachine.Update();
    }
}