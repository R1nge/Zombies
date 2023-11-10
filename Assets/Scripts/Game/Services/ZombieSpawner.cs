using Game.Services.Factories;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] positions;
        private UnitFactory _unitFactory;

        [Inject]
        private void Inject(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }
        
        public void Spawn()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                _unitFactory.CreateZombieUnit(positions[i].position, positions[i].rotation, null);
            }
        }
    }
}