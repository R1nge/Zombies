using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ZombieConfig", menuName = "Configs/Zombie Configs")]
    public class ZombieConfig : UnitConfig
    {
        [SerializeField] private Mesh zombieMesh;
        public Mesh ZombieMesh => zombieMesh;
    }
}