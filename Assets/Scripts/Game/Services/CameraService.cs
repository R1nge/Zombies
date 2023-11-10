using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Units;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private CinemachineVirtualCamera mapCamera, unitCamera;
        [SerializeField] private float flyThroughDuration;
        [SerializeField] private Transform flyThroughStart, flyThroughEnd; 
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController)
        {
            _unitRtsController = unitRtsController;
        }

        public void LookAtSelectedUnit()
        {
            mapCamera.Priority = 0;
            unitCamera.Priority = 1;
            unitCamera.Follow = _unitRtsController.SelectedUnit.transform;
            unitCamera.LookAt = _unitRtsController.SelectedUnit.transform;
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

        public IEnumerator FlyThrough()
        {
            mapCamera.Priority = 1;
            unitCamera.Priority = 0;
            yield return StartCoroutine(Lerp(flyThroughDuration));
        }
        
        private IEnumerator Lerp(float duration)
        {
            float time = 0;
            while (time < 1)
            {
                mapCamera.transform.position = Vector3.Lerp(flyThroughStart.position, flyThroughEnd.position, time);
                
                time += Time.deltaTime / duration;
                yield return null;
            }

            mapCamera.transform.position = flyThroughEnd.position;
        }
    }
}