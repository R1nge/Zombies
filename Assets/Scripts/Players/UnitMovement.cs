using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    public class UnitMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public UnitMovement(NavMeshAgent navMeshAgent) => _navMeshAgent = navMeshAgent;

        public void MoveTo(Vector3 position) => _navMeshAgent.SetDestination(position);
    }
}