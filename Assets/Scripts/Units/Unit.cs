using Data;
using Units.States;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitConfig unitConfig;
        private NavMeshAgent _navMeshAgent;
        private UnitStateMachine _unitStateMachine;
        private UnitMovement _unitMovement;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitConfig.Speed;

            _unitMovement = new UnitMovement(_navMeshAgent);

            _unitStateMachine = new UnitStateMachine(_unitMovement);
            _unitStateMachine.SetState(UnitStateMachine.UnitStates.Walking);
        }

        private void Update()
        {
            _unitStateMachine.Update();
        }
    }
}