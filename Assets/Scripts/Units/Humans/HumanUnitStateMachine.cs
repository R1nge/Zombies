using System.Collections.Generic;
using Units.Humans.States;
using Units.States;

namespace Units.Humans
{
    public class HumanUnitStateMachine
    {
        private readonly Dictionary<HumanUnitStates, IUnitState> _unitStates;
        private IUnitState _currentState;

        public HumanUnitStateMachine(UnitMovement unitMovement)
        {
            _unitStates = new Dictionary<HumanUnitStates, IUnitState>
            {
                { HumanUnitStates.Dead, new HumanUnitDead() }
            };
        }

        public void SetState(HumanUnitStates newState)
        {
            _currentState?.Exit();
            _currentState = _unitStates[newState];
            _currentState.Enter();
        }

        public void Update() => _currentState?.Update();

        public enum HumanUnitStates
        {
            Idle,
            Patrol,
            Chase,
            Attack,
            Dead
        }
    }
}