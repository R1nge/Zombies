using Units.Zombies;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Zombies Units Config", menuName = "Configs/Zombies Units")]
    public class ZombiesUnitsConfig : ScriptableObject
    {
        [SerializeField] private ZombieUnit zombie;
        [SerializeField] private ZombieUnit strongZombie;

        public ZombieUnit Zombie => zombie;
        public ZombieUnit StrongZombie => strongZombie;
    }
}