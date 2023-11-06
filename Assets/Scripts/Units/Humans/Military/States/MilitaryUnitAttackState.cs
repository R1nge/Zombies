using Units.States;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitAttackState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitMovement _unitMovement;
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;

        public MilitaryUnitAttackState(Transform transform, UnitMovement unitMovement, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _transform = transform;
            _unitMovement = unitMovement;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }
        
        public void Enter()
        {
        }

        public void Update()
        {
            //TODO: make it differently
            if (!_unitMovement.Target.TryGetComponent(out ZombieUnit zombieUnit) )
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Idle);
                return;
            }
            
            if (_unitMovement.DistanceToDestination() > 3f)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Chase);
            }
            else
            {
                Vector3 directionToTheTarget = _unitMovement.Target.position - _transform.position;
                
                var ray = new Ray(_transform.position, directionToTheTarget);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask: ~LayerMask.NameToLayer("ZombieUnit")))
                {
                    if (hit.transform.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(1);    
                    }
                }
                else
                {
                    _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Idle);
                }
            }
        }

        public void Exit()
        {
        }
    }
}