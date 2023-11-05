using Units.Zombies;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "HumanConfig", menuName = "Configs/Human Configs")]
    public class HumanConfig : UnitConfig
    {
        [SerializeField] private ZombieUnit zombie;
        public ZombieUnit Zombie => zombie;
    }
}