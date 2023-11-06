using Units.Zombies;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/Unit Config")]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] private ZombieUnit zombie;
        [SerializeField] private int maxHealth;
        [SerializeField] private float speed;
        public ZombieUnit Zombie => zombie;
        public int MaxHealth => maxHealth;
        public float Speed => speed;
    }
}