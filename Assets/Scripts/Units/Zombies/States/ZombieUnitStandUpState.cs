using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitStandUpState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnitStateMachine _zombieUnitStateMachine;

        public ZombieUnitStandUpState(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, ZombieUnitStateMachine zombieUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitAnimator = unitAnimator;
            _zombieUnitStateMachine = zombieUnitStateMachine;
        }

        public void Enter() { _coroutineRunner.StartCoroutine(StandUp()); }

        private IEnumerator StandUp()
        {
            _unitAnimator.PlayStandUpAnimation();
            yield return new WaitForSeconds(7f);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        public void Update() { }
        public void Exit() { }
    }
}