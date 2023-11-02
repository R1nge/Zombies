using System.Collections.Generic;
using Units.States;
using Units.Zombies.States;

namespace Units.Zombies
{
    public class ZombieUnitStateMachine
    {
        private readonly Dictionary<ZombieUnitStates, IUnitState> _unitStates;
        private IUnitState _currentState;

        public ZombieUnitStateMachine(UnitMovement unitMovement)
        {
            _unitStates = new Dictionary<ZombieUnitStates, IUnitState>
            {
                { ZombieUnitStates.Idle, new ZombieUnitIdleState() },
                { ZombieUnitStates.Walking, new ZombieUnitMoveState(unitMovement) },
                { ZombieUnitStates.Infecting, new ZombieUnitInfectState() },
                { ZombieUnitStates.Dead, new ZombieUnitDeadState() }
            };
        }

        public void SetState(ZombieUnitStates newState)
        {
            _currentState?.Exit();
            _currentState = _unitStates[newState];
            _currentState.Enter();
        }

        public void Update() => _currentState?.Update();

        public enum ZombieUnitStates
        {
            Idle,
            Walking,
            Infecting,
            Dead
        }
    }
}