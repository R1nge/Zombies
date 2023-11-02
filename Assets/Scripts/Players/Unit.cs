using Data;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        private NavMeshAgent _navMeshAgent;
        private UnitMovement _unitMovement;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = playerConfig.Speed;
            _unitMovement = new UnitMovement(_navMeshAgent);
        }

        private void Update()
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
    }
}