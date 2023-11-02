using UnityEngine;

namespace Units.Humans
{
    public class HumanUnit : Unit
    {
        private HumanUnitStateMachine _humanUnitStateMachine;

        protected override void Awake()
        {
            base.Awake();
            _humanUnitStateMachine = new HumanUnitStateMachine(UnitMovement);
        }

        protected override void Update() => _humanUnitStateMachine.Update();

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (collision.transform.TryGetComponent(out Unit unit))
            {
                _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
            }
        }
    }
}