using System;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public class HumanUnit : Unit
    {
        [SerializeField] private ZombieUnit zombieUnit;
        private HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitStateMachine.HumanUnitStates CurrentState => _humanUnitStateMachine.CurrentStateType;

        protected override void Awake()
        {
            base.Awake();
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, UnitAnimator, this, zombieUnit);
        }

        protected override void Update() => _humanUnitStateMachine.Update();

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (_humanUnitStateMachine.CurrentStateType is HumanUnitStateMachine.HumanUnitStates.Dead or HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie)
            {
                Debug.LogWarning("Human unit is already dead");
                return;
            }

            if (collision.transform.TryGetComponent(out ZombieUnit zombie))
            {
                Die();
            }
        }

        public override void StandUp() { throw new NotImplementedException(); }

        public override void Attack() { throw new NotImplementedException(); }
        public override void Die() { _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead); }
    }
}