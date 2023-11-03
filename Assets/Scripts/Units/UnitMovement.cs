using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class UnitMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public UnitMovement(NavMeshAgent navMeshAgent) => _navMeshAgent = navMeshAgent;

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
        }

        public void Stop() => _navMeshAgent.isStopped = true;
    }
}