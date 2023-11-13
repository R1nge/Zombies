using System.Collections;
using Game.Services;
using Units.States;
using UnityEngine;

namespace Units.Humans.Human.States
{
    public class HumanUnitDeadState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimator _unitAnimator;
        private readonly HumanUnitStateMachine _humanUnitStateMachine;
        private readonly ZombieCounter _zombieCounter;

        public HumanUnitDeadState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, HumanUnitStateMachine humanUnitStateMachine, ZombieCounter zombieCounter)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _humanUnitStateMachine = humanUnitStateMachine;
            _zombieCounter = zombieCounter;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            _zombieCounter.AddPending();
            _unitMovement.Stop();
            _unitAnimator.ApplyRootMotion(true);
            yield return new WaitForSeconds(1f);
            _unitAnimator.PlayDeathAnimation();
            yield return new WaitForSeconds(4f);
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie);
        }

        public void Update() { }

        public void Exit() { }
    }
}