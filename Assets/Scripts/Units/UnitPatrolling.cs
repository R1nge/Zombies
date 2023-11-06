using UnityEngine;

namespace Units
{
    public class UnitPatrolling
    {
        private readonly UnitMovement _unitMovement;
        private readonly Transform[] _points;
        private readonly float _delay;
        private float _currentDelay;
        private int _currentPoint;
        
        public UnitPatrolling(UnitMovement unitMovement, Transform[] points, float delay)
        {
            _unitMovement = unitMovement;
            _points = points;
            _delay = delay;
        }
        
        public void Patrol()
        {
            if (_currentDelay <= 0)
            {
                _currentDelay = _delay;
                SelectNextPatrolPosition();
            }
            else
            {
                _currentDelay -= Time.deltaTime;
            }
        }
        
        private void SelectNextPatrolPosition()
        {
            if (_currentPoint == _points.Length - 1)
            {
                _currentPoint = 0;
            }
            else
            {
                _currentPoint++;
            }

            
            _unitMovement.SetDestination(_points[_currentPoint].position);
            _unitMovement.MoveToDestination();
        }

    }
}