using System;
using System.Collections.Generic;
using Units.Zombies;
using UnityEngine;

namespace Units
{
    public class UnitRTSController : MonoBehaviour
    {
        [SerializeField] private LayerMask ignore;
        [SerializeField] private LayerMask unitLayer;
        [SerializeField] private RectTransform selectionArea;
        private Vector3 _startPosition;
        private Vector3 _startVisualPosition, _endVisualPosition;
        private readonly Collider[] _colliders = new Collider[20];
        private readonly List<ZombieUnit> _zombies = new();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSelect();
            }

            if (Input.GetMouseButton(0))
            {
                Select();
                DrawVisual();
            }

            if (Input.GetMouseButtonUp(0))
            {
                AddToList();
                HideSelectionArea();
            }

            if (Input.GetMouseButtonDown(1))
            {
                MoveTo();
            }
        }

        private void StartSelect()
        {
            selectionArea.gameObject.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: Single.MaxValue, layerMask: ~ignore))
            {
                _startPosition = hit.point;
            }

            _startVisualPosition = Input.mousePosition;
        }

        private void Select() => _endVisualPosition = Input.mousePosition;

        private void DrawVisual()
        {
            Vector2 boxStart = _startVisualPosition;
            Vector2 boxEnd = _endVisualPosition;

            Vector2 boxCenter = (boxStart + boxEnd) / 2f;
            selectionArea.position = boxCenter;

            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
            selectionArea.sizeDelta = boxSize;
        }

        private void HideSelectionArea() => selectionArea.gameObject.SetActive(false);

        private void AddToList()
        {
            _zombies.Clear();

            int size = Physics.OverlapBoxNonAlloc(_startPosition, selectionArea.transform.localScale, _colliders, Quaternion.identity, unitLayer);

            for (int i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent(out ZombieUnit zombieUnit))
                {
                    _zombies.Add(zombieUnit);
                }
            }

            print($"SELECTED {size} zombies");
        }

        private void MoveTo()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 currentMousePosition = hit.point;
                for (int i = 0; i < _zombies.Count; i++)
                {
                    _zombies[i].MoveTo(currentMousePosition);
                }
            }
        }
    }
}