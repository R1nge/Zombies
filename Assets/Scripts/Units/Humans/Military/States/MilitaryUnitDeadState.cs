using System.Collections;
using Game.Services;
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
        private readonly ZombieCounter _zombieCounter;
        private readonly Sensor _sensor;

        public MilitaryUnitDeadState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, MilitaryUnitStateMachine militaryUnitStateMachine, ZombieCounter zombieCounter, Sensor sensor)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _militaryUnitStateMachine = militaryUnitStateMachine;
            _zombieCounter = zombieCounter;
            _sensor = sensor;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(Wait());

        private IEnumerator Wait()
        {
            //TODO: disable sensor
            _sensor.Disable();
            _zombieCounter.AddPending();
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