using System.Collections;
using Game.Services;
using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitStandUpState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnitStateMachine _zombieUnitStateMachine;
        private readonly UnitMovement _unitMovement;

        public ZombieUnitStandUpState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, ZombieUnitStateMachine zombieUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _zombieUnitStateMachine = zombieUnitStateMachine;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(StandUp());

        private IEnumerator StandUp()
        {
            _unitMovement.Stop();
            _unitAnimator.PlayStandUpAnimation();
            yield return new WaitForSeconds(5f);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        public void Update() { }
        public void Exit() { }
    }
}