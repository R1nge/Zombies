using UnityEngine;

namespace Units.States
{
    public class UnitMoveState : IUnitState
    {
        private readonly UnitMovement _unitMovement;

        public UnitMoveState(UnitMovement unitMovement) => _unitMovement = unitMovement;

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
        }

        public void Exit() { }
    }
}