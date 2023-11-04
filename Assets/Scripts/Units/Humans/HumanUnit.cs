using System;
using Data;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public class HumanUnit : Unit
    {
        [SerializeField] private HumanConfig humanConfig;
        private ZombieUnit _zombieUnit;
        private HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitStateMachine.HumanUnitStates CurrentState => _humanUnitStateMachine.CurrentStateType;

        private void OnEnable()
        {
            skinnedMeshRenderer.sharedMesh = humanConfig.HumanMesh;
            NavMeshAgent.speed = humanConfig.Speed;
        }

        protected override void Awake()
        {
            base.Awake();

            _zombieUnit = GetComponent<ZombieUnit>();
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, UnitAnimator, this, _zombieUnit);
        }

        protected override void Update() => _humanUnitStateMachine.Update();

        public override void StandUp() { }
        public override void Attack() { }
        public override void Die() => _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
    }
}