using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Services
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask ground;
        [SerializeField] private Button selectNext, selectPrevious;
        private float _mousePressedTime;
        private Vector2 _startMousePosition;
        private UnitRTSController _unitRtsController;
        private CameraService _cameraService;

        [Inject]
        private void Inject(UnitRTSController unitRtsController, CameraService cameraService)
        {
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
        }

        private void Awake()
        {
            selectNext.onClick.AddListener(SelectNextUnit);
            selectPrevious.onClick.AddListener(SelectPreviousUnit);
            _unitRtsController.OnZombiesAmountChanged += ZombiesAmountChanged;
        }

        private void ZombiesAmountChanged(int previousAmount, int amount)
        {
            if (previousAmount > 1 && amount <= 1)
            {
                selectNext.gameObject.SetActive(false);
                selectPrevious.gameObject.SetActive(false);
                SelectPreviousUnit();
            }
            else
            {
                selectNext.gameObject.SetActive(true);
                selectPrevious.gameObject.SetActive(true);
            }
        }

        private void Update() => MoveUnits();

        private void SelectNextUnit()
        {
            _unitRtsController.SelectNext();
            _cameraService.LookAtSelectedUnit();
        }

        private void SelectPreviousUnit()
        {
            _unitRtsController.SelectPrevious();
            _cameraService.LookAtSelectedUnit();
        }

        private void MoveUnits()
        {
            if (_unitRtsController.SelectedUnit == null)
            {
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    _cameraService.Raycast(Input.mousePosition, out RaycastHit hit, 100, layerMask: ground);
                    _unitRtsController.SelectedUnit.MoveTo(hit.point);
                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _cameraService.Raycast(Input.GetTouch(0).position, out RaycastHit hit, 100, layerMask: ground);
                        _unitRtsController.SelectedUnit.MoveTo(hit.point);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _unitRtsController.OnZombiesAmountChanged -= ZombiesAmountChanged;
            selectNext.onClick.RemoveAllListeners();
            selectPrevious.onClick.RemoveAllListeners();
        }
    }
}