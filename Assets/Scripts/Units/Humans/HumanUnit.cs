using System.Collections;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public class HumanUnit : Unit
    {
        [SerializeField] private ZombieUnit zombieUnit;
        [SerializeField] private ZombieUnit mesh;
        private HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitStateMachine.HumanUnitStates CurrentState => _humanUnitStateMachine.CurrentStateType;
        
        protected override void Awake()
        {
            base.Awake();
            _humanUnitStateMachine = new HumanUnitStateMachine(UnitMovement);
        }

        protected override void Update() => _humanUnitStateMachine.Update();

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (_humanUnitStateMachine.CurrentStateType == HumanUnitStateMachine.HumanUnitStates.Dead)
            {
                Debug.LogWarning("Human unit is already dead");
                return;
            }

            if (collision.transform.TryGetComponent(out Unit unit))
            {
                _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
            }
        }

        public override void StandUp() { throw new System.NotImplementedException(); }

        public override void Infect() { throw new System.NotImplementedException(); }
        public override void Die() { throw new System.NotImplementedException(); }
    }
}