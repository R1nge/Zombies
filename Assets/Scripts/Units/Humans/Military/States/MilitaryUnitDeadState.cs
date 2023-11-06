using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitDeadState : IUnitState
    {
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;
        private readonly UnitAnimator _unitAnimator;
        private readonly CoroutineRunner _coroutineRunner;

        public MilitaryUnitDeadState(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitAnimator = unitAnimator;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }

        public void Enter()
        {
            
            _coroutineRunner.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            _unitAnimator.PlayDeathAnimation();
            yield return new WaitForSeconds(5f);
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }

        public void Update() { }

        public void Exit() { }
    }
}