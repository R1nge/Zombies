using Game.Services;
using Units.Zombies;
using UnityEngine;
using Zenject;

namespace Units.Humans.Military
{
    public class MilitaryUnit : Unit
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float nextPatrolPointInterval;
        private UnitSoundsController _unitSoundsController;
        private MilitaryUnitStateMachine _militaryUnitStateMachine;
        private HumanCounter _humanCounter;

        private MilitaryUnitStateMachine.MilitaryUnitStates CurrentState => _militaryUnitStateMachine.CurrentStateType;

        [Inject]
        private void Inject(HumanCounter humanCounter) => _humanCounter = humanCounter;

        protected override void Awake()
        {
            base.Awake();
            _unitSoundsController = GetComponent<UnitSoundsController>();
            UnitPatrolling unitPatrolling = new UnitPatrolling(UnitMovement, patrolPoints, nextPatrolPointInterval);
            _militaryUnitStateMachine = new MilitaryUnitStateMachine(CoroutineRunner, this, transform, UnitMovement, unitPatrolling, UnitAnimator, unitConfig, UnitFactory, _unitSoundsController);
            _humanCounter.Add();

            Patrol();
        }

        protected override void Update()
        {
            base.Update();
            _militaryUnitStateMachine.Update();
            print($"MILITARY: Current state {_militaryUnitStateMachine.CurrentStateType}");
        }

        public override void Idle()
        {
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Idle);
        }

        public void Patrol()
        {
            if (patrolPoints.Length != 0)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Patrol);
            }
        }

        public void Chase(ZombieUnit zombieUnit)
        {
            UnitMovement.SetDestination(zombieUnit.transform.position);
            UnitMovement.SetTarget(zombieUnit.transform);
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Chase);
        }

        public override void Die()
        {
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Dead);
            _humanCounter.Remove();
        }

        public override bool CanBeAttackedBy(Unit unit)
        {
            return CurrentState is not (MilitaryUnitStateMachine.MilitaryUnitStates.Dead or MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }
    }
}