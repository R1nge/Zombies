namespace Units.Zombies
{
    public class ZombieUnit : Unit
    {
        private ZombieUnitStateMachine _zombieUnitStateMachine;
        
        protected override void Awake()
        {
            base.Awake();
            _zombieUnitStateMachine = new ZombieUnitStateMachine(UnitMovement);
            _zombieUnitStateMachine.SetState(ZombieUnitStateMachine.ZombieUnitStates.Walking);
        }

        protected override void Update() => _zombieUnitStateMachine.Update();
    }
}