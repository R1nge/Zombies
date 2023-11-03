using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitMoveState : IUnitState
    {
        private readonly UnitMovement _unitMovement;

        public ZombieUnitMoveState(UnitMovement unitMovement) => _unitMovement = unitMovement;

        public void Enter() { }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _unitMovement.MoveTo(hit.point);
                }
            }
            
            Debug.Log("MOVE UPDATE");
        }

        public void Exit()
        {
            _unitMovement.Stop();
        }
    }
}