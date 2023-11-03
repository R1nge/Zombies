using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitInfectState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnitStateMachine _zombieUnitStateMachine;

        public ZombieUnitInfectState(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, ZombieUnitStateMachine zombieUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitAnimator = unitAnimator;
            _zombieUnitStateMachine = zombieUnitStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log("ENTER INFECT");
            _unitAnimator.PlayAttackAnimation();
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