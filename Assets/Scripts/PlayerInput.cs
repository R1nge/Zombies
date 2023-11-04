using Units;
using Units.Zombies;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float delayBeforeDragSelection = .1f;
    private float _mousePressedTime;
    private Vector2 _startMousePosition;
    private UnitRTSController _unitRtsController;

    private void Awake() => _unitRtsController = FindObjectOfType<UnitRTSController>();

    private void Update()
    {
        SelectUnits();
        MoveUnits();
    }

    private void SelectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectionBox.gameObject.SetActive(true);
            _startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (_mousePressedTime + delayBeforeDragSelection < Time.time)
            {
                ResizeSelectionBox();
            }

            _mousePressedTime = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);


            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, unitLayer))
            {
                if (hit.transform.TryGetComponent(out ZombieUnit zombieUnit))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (_unitRtsController.IsSelected(zombieUnit))
                        {
                            _unitRtsController.DeSelect(zombieUnit);
                        }
                        else
                        {
                            _unitRtsController.Select(zombieUnit);
                        }
                    }
                    else
                    {
                        _unitRtsController.DeSelectAll();
                        _unitRtsController.Select(zombieUnit);
                    }
                }
                else if (_mousePressedTime + delayBeforeDragSelection > Time.time)
                {
                    _unitRtsController.DeSelectAll();
                }
            }
        }
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - _startMousePosition.x;
        float height = Input.mousePosition.y - _startMousePosition.y;

        selectionBox.anchoredPosition = _startMousePosition + new Vector2(width / 2, height / 2);
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        var bounds = new Bounds(selectionBox.anchoredPosition, selectionBox.sizeDelta);

        for (int i = 0; i < _unitRtsController.AvailableUnits.Count; i++)
        {
            Vector3 unitPosition =
                camera.WorldToScreenPoint(_unitRtsController.AvailableUnits[i].transform.position);

            if (UnitInsideSelectBox(unitPosition, bounds))
            {
                _unitRtsController.Select(_unitRtsController.AvailableUnits[i]);
            }
            else
            {
                _unitRtsController.DeSelect(_unitRtsController.AvailableUnits[i]);
            }
        }
    }

    private bool UnitInsideSelectBox(Vector2 position, Bounds bounds)
    {
        bool x = position.x > bounds.min.x && position.x < bounds.max.x;
        bool y = position.y > bounds.min.y && position.y < bounds.max.y;

        return x && y;
    }

    private void MoveUnits()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (_unitRtsController.SelectedUnits.Count > 0)
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask: ground))
                {
                    foreach (var zombieUnit in _unitRtsController.SelectedUnits)
                    {
                        zombieUnit.MoveTo(hit.point);
                    }
                }
            }
        }
    }
}