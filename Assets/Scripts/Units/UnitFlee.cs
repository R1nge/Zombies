using Units.Humans.Human;
using UnityEngine;

namespace Units
{
    public class UnitFlee
    {
        private readonly HumanUnit _humanUnit;
        private readonly UnitMovement _unitMovement;
        private readonly Transform _transform;
        private Transform _target;

        public UnitFlee(HumanUnit humanUnit, UnitMovement unitMovement, Transform transform)
        {
            _humanUnit = humanUnit;
            _unitMovement = unitMovement;
            _transform = transform;
        }

        public void SetTarget(Transform target) => _target = target;

        private void ResetTarget()
        {
            _target = null;
            _unitMovement.Stop();
        }

        public void Flee()
        {
            if (_target != null)
            {
                Vector3 direction = _target.transform.position - _transform.position;
                _unitMovement.SetDestination(_transform.position - direction.normalized);

                if (Vector3.Distance(_target.position, _transform.position) >= 10)
                {
                    ResetTarget();
                    _humanUnit.Idle();
                }
            }
        }
    }
}