using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Humans.States
{
    public class HumanUnitDead : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitAnimator _unitAnimator;
        private readonly HumanUnit _humanUnit;
        private readonly HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitDead(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, HumanUnit humanUnit, HumanUnitStateMachine humanUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitAnimator = unitAnimator;
            _humanUnit = humanUnit;
            _humanUnitStateMachine = humanUnitStateMachine;
        }

        public void Enter()
        {
            _unitAnimator.PlayDeathAnimation();
            _humanUnit.enabled = false;
            _coroutineRunner.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(4f);
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie);
        }

        public void Update() { }

        public void Exit() { }
    }
}