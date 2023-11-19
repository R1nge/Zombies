using Units.States;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitAttackState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        private readonly Transform _transform;
        private readonly MilitaryUnit _militaryUnit;
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;
        private readonly UnitSoundsController _unitSoundsController;
        private readonly UnitAnimator _unitAnimator;

        public MilitaryUnitAttackState(MilitaryUnit militaryUnit, Transform transform, UnitMovement unitMovement, UnitSoundsController unitSoundsController, MilitaryUnitStateMachine militaryUnitStateMachine, UnitAnimator unitAnimator)
        {
            _militaryUnit = militaryUnit;
            _transform = transform;
            _unitMovement = unitMovement;
            _unitSoundsController = unitSoundsController;
            _militaryUnitStateMachine = militaryUnitStateMachine;
            _unitAnimator = unitAnimator;
        }

        public void Enter() { }

        public void Update()
        {
            //TODO: make it differently (Store a reference to a zombie unit???)
            if (!_unitMovement.Target.TryGetComponent(out ZombieUnit zombieUnit))
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Idle);
                return;
            }

            if (_unitMovement.DistanceToDestination() > _militaryUnit.AttackDistance)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Chase);
            }
            else
            {
                Vector3 directionToTheTarget = _unitMovement.Target.position - _transform.position;

                var ray = new Ray(_transform.position, directionToTheTarget);

                //TODO: add a delay between shots, spawn bullets (Attack controller)

                if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask: ~LayerMask.NameToLayer("ZombieUnit")))
                {
                    _transform.LookAt(hit.transform);
                    _unitSoundsController.PlayFireSound();
                    _unitAnimator.ApplyRootMotion(false);
                    _unitAnimator.PlayAttackAnimation();
                    
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

        public void Exit() { }
    }
}