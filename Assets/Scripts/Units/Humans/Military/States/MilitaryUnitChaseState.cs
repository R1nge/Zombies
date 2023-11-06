using Units.States;
using UnityEngine;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitChaseState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;

        public MilitaryUnitChaseState(UnitMovement unitMovement, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _unitMovement = unitMovement;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }

        public void Enter() { }

        public void Update()
        {
            Debug.Log($"Distance to destination: {_unitMovement.DistanceToDestination()}");
            if (_unitMovement.DistanceToDestination() < 3f)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Attack);
            }
            else
            {
                _unitMovement.MoveToDestination();
            }
        }

        public void Exit() { }
    }
}