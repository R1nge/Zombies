using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class UnitMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        private Transform _target;
        private Vector3 _targetPosition;
        
        public UnitMovement(NavMeshAgent navMeshAgent) => _navMeshAgent = navMeshAgent;

        public Transform Target => _target;

        public Vector3 TargetPosition => _targetPosition;
        
        public float CurrentSpeed => _navMeshAgent.velocity.magnitude;

        public void SetDestination(Vector3 position)
        {
            _targetPosition = position;
            _navMeshAgent.SetDestination(position);
        }

        public void MoveToDestination() => _navMeshAgent.isStopped = false;

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _targetPosition = _navMeshAgent.transform.position;
            SetDestination(_targetPosition);
        }

        //TODO: Create a separate method for the distance to the target?
        public float DistanceToDestination()
        {
            if (_target != null)
            {
                return Vector3.Distance(_navMeshAgent.transform.position, _target.position);
            }

            return Vector3.Distance(_navMeshAgent.transform.position, _navMeshAgent.destination);
        }

        public void SetTarget(Transform target) => _target = target;

        public void ResetTarget() => _target = null;
    }
}