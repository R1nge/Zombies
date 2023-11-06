using System;
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

        private void Start()
        {
            _unitRtsController.Add(this);
            NavMeshAgent.speed = unitConfig.Speed;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (collision.transform.TryGetComponent(out ZombieUnit zombieUnit))
            {
                return;
            }

            if (collision.transform.TryGetComponent(out Unit unit))
            {
                if (!unit.CanBeAttacked())
                {
                    Debug.LogWarning($"{unit.gameObject.name} can't be attacked");
                    return;
                }

                if (IsBehind(unit.transform))
                {
                    SetTargetForward(-DirectionFromPlayerTo(unit.transform));
                    unit.SetTargetForward(-DirectionFromPlayerTo(unit.transform));
                    Attack();
                    unit.Die();
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

        public override void Idle()
        {
            throw new NotImplementedException();
        }

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