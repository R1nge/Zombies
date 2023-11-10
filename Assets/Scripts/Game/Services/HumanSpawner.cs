using UnityEngine;

namespace Game.Services
{
    public class HumanSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] positions;

        public void Spawn()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                
            }
        }
    }
}