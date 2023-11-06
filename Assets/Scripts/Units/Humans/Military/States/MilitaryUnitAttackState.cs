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
        private readonly UnitSoundsController _unitSoundsController;

        public MilitaryUnitAttackState(Transform transform, UnitMovement unitMovement, UnitSoundsController unitSoundsController, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _transform = transform;
            _unitMovement = unitMovement;
            _unitSoundsController = unitSoundsController;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }
        
        public void Enter()
        {
        }

        public void Update()
        {
            //TODO: make it differently (Store a reference to a zombie unit???)
            if (!_unitMovement.Target.TryGetComponent(out ZombieUnit zombieUnit) )
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Idle);
                return;
            }
            
            if (_unitMovement.DistanceToDestination() > 5f)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Chase);
            }
            else
            {
                Vector3 directionToTheTarget = _unitMovement.Target.position - _transform.position;
                
                var ray = new Ray(_transform.position, directionToTheTarget);
                
                //TODO: add a delay between shots, spawn bullets (Attack controller)
                _unitSoundsController.PlayFireSound();
                
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