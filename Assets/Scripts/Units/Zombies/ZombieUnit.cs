using Units.Humans;
using UnityEngine;

namespace Units.Zombies
{
    public class ZombieUnit : Unit
    {
        private ZombieUnitStateMachine _zombieUnitStateMachine;

        protected override void Awake()
        {
            base.Awake();
            _zombieUnitStateMachine = new ZombieUnitStateMachine(CoroutineRunner,  UnitMovement, UnitAnimator);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.transform.TryGetComponent(out HumanUnit humanUnit))
            {
                if (humanUnit.CurrentState == HumanUnitStateMachine.HumanUnitStates.Dead)
                {
                    Debug.LogWarning("Human unit is already dead");
                    return;
                }

                Infect();
            }
        }

        public override void StandUp() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.StandUp);

        public override void Infect() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Infecting);

        public override void Die() => _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Dead);

        protected override void Update() => _zombieUnitStateMachine.Update();
    }
}