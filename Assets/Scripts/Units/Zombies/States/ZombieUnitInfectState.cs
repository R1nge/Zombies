using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitInfectState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnitStateMachine _zombieUnitStateMachine;

        public ZombieUnitInfectState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, ZombieUnitStateMachine zombieUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _zombieUnitStateMachine = zombieUnitStateMachine;
        }
        
        public void Enter()
        {
            _unitAnimator.PlayAttackAnimation();
            _unitMovement.Stop();
            _coroutineRunner.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        public void Update() { }

        public void Exit() { }
    }
}