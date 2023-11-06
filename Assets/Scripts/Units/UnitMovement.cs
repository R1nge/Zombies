using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class UnitMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public UnitMovement(NavMeshAgent navMeshAgent) => _navMeshAgent = navMeshAgent;

        public float CurrentSpeed => _navMeshAgent.velocity.magnitude;

        public Vector3 CurrentDestination => _navMeshAgent.destination;

        public void SetDestination(Vector3 position) => _navMeshAgent.SetDestination(position);

        public void MoveToDestination() => _navMeshAgent.isStopped = false;

        public void Stop() => _navMeshAgent.isStopped = true;

        public float DistanceToDestination() => Vector3.Distance(_navMeshAgent.transform.position, _navMeshAgent.destination);
    }
}