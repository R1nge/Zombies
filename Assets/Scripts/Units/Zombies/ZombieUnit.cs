using System;
using Units.Humans.Human;
using Units.Humans.Military;
using UnityEngine;
using Zenject;

namespace Units.Zombies
{
    public class ZombieUnit : Unit
    {
        [SerializeField] private GameObject selectedMark;
        private ZombieUnitStateMachine _zombieUnitStateMachine;
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController) => _unitRtsController = unitRtsController;

        protected override void Awake()
        {
            base.Awake();
            _zombieUnitStateMachine = new ZombieUnitStateMachine(CoroutineRunner, UnitMovement, UnitAnimator);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Idle);
        }

        private void OnEnable()
        {
            _unitRtsController.Add(this);
            NavMeshAgent.speed = unitConfig.Speed;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.transform.TryGetComponent(out MilitaryUnit militaryUnit))
            {
                if (!militaryUnit.CanBeAttacked())
                {
                    Debug.LogWarning("Military unit can't be attacked");
                    return;
                }

                if(IsBehind(militaryUnit.transform))
                {
                    SetTargetForward(-DirectionFromPlayerTo(militaryUnit.transform));
                    militaryUnit.SetTargetForward(-DirectionFromPlayerTo(militaryUnit.transform));
                    Attack();
                    militaryUnit.Die();
                }
            }
            
            if (collision.transform.TryGetComponent(out HumanUnit humanUnit))
            {
                if (!humanUnit.CanBeAttacked())
                {
                    Debug.LogWarning("Human unit can't be attacked");
                    return;
                }

                if(IsBehind(humanUnit.transform))
                {
                    SetTargetForward(-DirectionFromPlayerTo(humanUnit.transform));
                    humanUnit.SetTargetForward(-DirectionFromPlayerTo(humanUnit.transform));
                    Attack();
                    humanUnit.Die();
                }
            }
        }

        public void OnSelected() => selectedMark.SetActive(true);

        public void OnDeselected() => selectedMark.SetActive(false);

        //TODO: zombie can't move when spawned for some reason
        public void MoveTo(Vector3 position)
        {
            UnitMovement.SetDestination(position);

            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.Infecting)
            {
                return;
            }

            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.Dead)
            {
                return;
            }

            if (_zombieUnitStateMachine.CurrentStateType == ZombieUnitStateMachine.ZombieUnitStates.StandUp)
            {
                return;
            }

            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        public override void Idle() { throw new NotImplementedException(); }

        public void StandUp() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.StandUp);

        public void Attack() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Infecting);

        public override void Die() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Dead);
        public override bool CanBeAttacked()
        {
            return _zombieUnitStateMachine.CurrentStateType is not (ZombieUnitStateMachine.ZombieUnitStates.Dead
                or ZombieUnitStateMachine.ZombieUnitStates.Infecting);
        }

        protected override void Update()
        {
            base.Update();
            _zombieUnitStateMachine.Update();
            Debug.Log($"Current state: {_zombieUnitStateMachine.CurrentStateType}");
        }
    }
}