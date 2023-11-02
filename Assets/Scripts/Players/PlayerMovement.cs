using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    public class PlayerMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public PlayerMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }
    }
}