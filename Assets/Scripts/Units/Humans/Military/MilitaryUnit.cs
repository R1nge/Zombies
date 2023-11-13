using System.Collections.Generic;
using Game.Services;
using Units.Zombies;
using UnityEngine;
using Zenject;

namespace Units.Humans.Military
{
    public class MilitaryUnit : Unit
    {
        [SerializeField] private float nextPatrolPointInterval;
        private readonly List<Transform> _patrolPoints = new();
        private UnitSoundsController _unitSoundsController;
        private MilitaryUnitStateMachine _militaryUnitStateMachine;
        private HumanCounter _humanCounter;
        private ZombieCounter _zombieCounter;

        private MilitaryUnitStateMachine.MilitaryUnitStates CurrentState => _militaryUnitStateMachine.CurrentStateType;

        [Inject]
        private void Inject(HumanCounter humanCounter, ZombieCounter zombieCounter)
        {
            _humanCounter = humanCounter;
            _zombieCounter = zombieCounter;
        }

        protected override void Awake()
        {
            base.Awake();
            _unitSoundsController = GetComponent<UnitSoundsController>();
            UnitPatrolling unitPatrolling = new UnitPatrolling(UnitMovement, _patrolPoints, nextPatrolPointInterval);
            _militaryUnitStateMachine = new MilitaryUnitStateMachine(CoroutineRunner, this, transform, UnitMovement, unitPatrolling, UnitAnimator, UnitFactory, _unitSoundsController, _zombieCounter);
            _humanCounter.Add();
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
            if (_patrolPoints.Count != 0)
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

        public void SetPatrolPath()
        {
            Transform patrol = transform.parent.Find("PatrolPath");

            if (patrol != null)
            {
                for (int i = 0; i < patrol.childCount; i++)
                {
                    _patrolPoints.Add(patrol.GetChild(i));
                }
            }
        }
    }
}