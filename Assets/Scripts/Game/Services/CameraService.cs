using Cinemachine;
using Units;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController)
        {
            _unitRtsController = unitRtsController;
        }

        public void LookAtSelectedUnit()
        {
            cinemachineVirtualCamera.Follow = _unitRtsController.SelectedUnit.transform;
            cinemachineVirtualCamera.LookAt = _unitRtsController.SelectedUnit.transform;
        }

        public Vector3 Raycast(Vector3 position, out RaycastHit hit, float distance, LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(position);
            
            if(Physics.Raycast(ray, out hit, distance, layerMask: layerMask))
            {
                return hit.point;
            }
            
            Debug.LogError("Camera service: ray hit nothing");
            
            return Vector3.zero;
        }
    }
}