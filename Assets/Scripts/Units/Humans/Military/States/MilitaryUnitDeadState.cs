using System.Collections;
using Units.States;
using UnityEngine;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitDeadState : IUnitState
    {
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimator _unitAnimator;
        private readonly CoroutineRunner _coroutineRunner;

        public MilitaryUnitDeadState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(Wait());

        private IEnumerator Wait()
        {
            _unitMovement.Stop();
            yield return new WaitForSeconds(1f);
            _unitAnimator.PlayDeathAnimation();
            yield return new WaitForSeconds(5f);
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }

        public void Update() { }

        public void Exit() { }
    }
}