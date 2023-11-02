using Data;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        private NavMeshAgent _navMeshAgent;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = playerConfig.Speed;
            _playerMovement = new PlayerMovement(_navMeshAgent);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _playerMovement.MoveTo(hit.point);    
                }
            }
        }
    }
}