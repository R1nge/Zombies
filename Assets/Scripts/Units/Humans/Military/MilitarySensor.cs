using Units.Zombies;

namespace Units.Humans.Military
{
    public class MilitarySensor : Sensor
    {
        private MilitaryUnit _militaryUnit;
        private void Awake() => _militaryUnit = GetComponent<MilitaryUnit>();
        protected override void OnZombieSeen(ZombieUnit zombieUnit) => _militaryUnit.Chase(zombieUnit);
    }
}