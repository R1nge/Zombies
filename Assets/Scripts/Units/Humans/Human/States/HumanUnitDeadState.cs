using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Humans.Human.States
{
    public class HumanUnitDeadState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitAnimator _unitAnimator;
        private readonly HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitDeadState(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, HumanUnitStateMachine humanUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitAnimator = unitAnimator;
            _humanUnitStateMachine = humanUnitStateMachine;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
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