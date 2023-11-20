using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Services
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask ground, ignore;
        [SerializeField] private Button selectNext, selectPrevious;
        private int _groundLayerConverted;
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
            _groundLayerConverted = (int)Mathf.Log(ground.value, 2);
            selectNext.onClick.AddListener(SelectNextUnit);
            selectPrevious.onClick.AddListener(SelectPreviousUnit);
            _unitRtsController.OnZombiesAmountChanged += ZombiesAmountChanged;
            _unitRtsController.OnPendingUnitsAmountChanged += PendingAmountChanged;
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

        private void PendingAmountChanged(int previousAmount, int amount)
        {
            if (previousAmount == 1 && _unitRtsController.AvailableUnitsCount == 0)
            {
                selectNext.gameObject.SetActive(true);
                selectPrevious.gameObject.SetActive(true);
            }
            else
            {
                selectNext.gameObject.SetActive(false);
                selectPrevious.gameObject.SetActive(false);
                SelectPreviousUnit();
            }
        }

        private void Update() => MoveUnits();

        private void SelectNextUnit()
        {
            if (_unitRtsController.SelectNext())
            {
                _cameraService.LookAtSelectedUnit();
            }
        }

        private void SelectPreviousUnit()
        {
            if (_unitRtsController.SelectPrevious())
            {
                _cameraService.LookAtSelectedUnit();
            }
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
                    _cameraService.Raycast(Input.mousePosition, out RaycastHit hit, 100, ~ignore);

                    if (hit.transform.gameObject.layer != _groundLayerConverted)
                    {
                        Debug.LogError($"Hit is not ground. {hit.transform.gameObject.layer}");
                        return;
                    }

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
                        _cameraService.Raycast(Input.GetTouch(0).position, out RaycastHit hit, 100, ~ignore);

                        if (hit.transform.gameObject.layer != _groundLayerConverted)
                        {
                            Debug.LogError($"Hit is not ground. {hit.transform.gameObject.layer}");
                            return;
                        }

                        _unitRtsController.SelectedUnit.MoveTo(hit.point);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _unitRtsController.OnZombiesAmountChanged -= ZombiesAmountChanged;
            _unitRtsController.OnPendingUnitsAmountChanged -= PendingAmountChanged;
            selectNext.onClick.RemoveAllListeners();
            selectPrevious.onClick.RemoveAllListeners();
        }
    }
}