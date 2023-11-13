using Units.States;

namespace Units.Zombies.States
{
    public class ZombieUnitDeadState : IUnitState
    {
        private readonly ZombieDeathController _zombieDeathController;

        public ZombieUnitDeadState(ZombieDeathController zombieDeathController) => _zombieDeathController = zombieDeathController;

        public void Enter() => _zombieDeathController.Die();

        public void Update() { }

        public void Exit() { }
    }
}