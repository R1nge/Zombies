using Units.Zombies;

namespace Units.Humans.Human
{
    public class HumanSensor : Sensor
    {
        private HumanUnit _humanUnit;
        private void Awake() => _humanUnit = GetComponent<HumanUnit>();
        protected override void OnZombieSeen(ZombieUnit zombieUnit) => _humanUnit.Flee();
    }
}