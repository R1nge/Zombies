using System.Collections;
using Cinemachine;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Services
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private LayerMask ground;
        [SerializeField] private Button selectNext, selectPrevious;
        private float _mousePressedTime;
        private Vector2 _startMousePosition;
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController) => _unitRtsController = unitRtsController;

        private void Awake()
        {
            selectNext.onClick.AddListener(SelectNextUnit);
            selectPrevious.onClick.AddListener(SelectPreviousUnit);
            _unitRtsController.OnZombiesAmountChanged += ZombiesAmountChanged;
        }

        private void ZombiesAmountChanged(int previous, int amount)
        {
            if (amount <= 1 && previous <= 1)
            {
                selectNext.gameObject.SetActive(false);
                selectPrevious.gameObject.SetActive(false);
            }
            else
            {
                selectNext.gameObject.SetActive(true);
                selectPrevious.gameObject.SetActive(true);
            }
        }

        //TODO: fix execution order using state machine
        private IEnumerator Start()
        {
            yield return null;
            _unitRtsController.SelectFirst();
        }

        private void Update() => MoveUnits();

        private void SelectNextUnit()
        {
            _unitRtsController.SelectNext();
            cinemachineVirtualCamera.Follow = _unitRtsController.SelectedUnit.transform;
            cinemachineVirtualCamera.LookAt = _unitRtsController.SelectedUnit.transform;
        }

        private void SelectPreviousUnit()
        {
            _unitRtsController.SelectPrevious();
            cinemachineVirtualCamera.Follow = _unitRtsController.SelectedUnit.transform;
            cinemachineVirtualCamera.LookAt = _unitRtsController.SelectedUnit.transform;
        }

        private void MoveUnits()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask: ground))
                    {
                        _unitRtsController.SelectedUnit.MoveTo(hit.point);
                    }
                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = camera.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask: ground))
                        {
                            _unitRtsController.SelectedUnit.MoveTo(hit.point);
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            selectNext.onClick.RemoveAllListeners();
            selectPrevious.onClick.RemoveAllListeners();
        }
    }
}