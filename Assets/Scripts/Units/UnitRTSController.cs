using System;
using System.Collections.Generic;
using Units.Zombies;
using UnityEngine;

namespace Units
{
    public class UnitRTSController
    {
        //previous, value
        public event Action<int, int> OnZombiesAmountChanged;
        public event Action<int, int> OnPendingUnitsAmountChanged; 
        public int AvailableUnitsCount => _availableUnits.Count;
        public int PendingUnitsCount => _pendingUnits.Count;
        public ZombieUnit SelectedUnit => _selectedUnit;
        private readonly List<ZombieUnit> _availableUnits = new();
        private readonly List<GameObject> _pendingUnits = new();
        private ZombieUnit _selectedUnit;
        private int _selectedUnitIndex;

        public void Add(ZombieUnit zombieUnit)
        {
            int previous = _availableUnits.Count;
            _availableUnits.Add(zombieUnit); 
            OnZombiesAmountChanged?.Invoke(previous, _availableUnits.Count);
        }
        
        public void AddPending(GameObject gameObject)
        {
            int previous = _pendingUnits.Count;
            _pendingUnits.Add(gameObject);
            OnPendingUnitsAmountChanged?.Invoke(previous, _pendingUnits.Count);
        }

        public void Remove(ZombieUnit zombieUnit)
        {
            DeSelect();
            int previous = _availableUnits.Count;
            _availableUnits.Remove(zombieUnit);
            _selectedUnit = null;
            OnZombiesAmountChanged?.Invoke(previous, _availableUnits.Count);
        }
        
        public void RemovePending(GameObject gameObject)
        {
            int previous = _pendingUnits.Count;
            _pendingUnits.Remove(gameObject);
            OnPendingUnitsAmountChanged?.Invoke(previous, _pendingUnits.Count);
        }

        public void SelectFirst()
        {
            _selectedUnit = _availableUnits[0];
            _selectedUnit.OnSelected();
        }

        public bool SelectNext()
        {
            if (_availableUnits.Count == 0)
            {
                return false;
            }
            
            _selectedUnitIndex = (_selectedUnitIndex + 1) % _availableUnits.Count;
            ZombieUnit zombieUnit = _availableUnits[_selectedUnitIndex];
            
            if (!IsSelected(zombieUnit))
            {
                DeSelect();
                _selectedUnit = zombieUnit;
                zombieUnit.OnSelected();

                return true;
            }

            return false;
        }

        public bool SelectPrevious()
        {
            if (_availableUnits.Count == 0)
            {
                return false;
            }
            
            if (_selectedUnitIndex - 1 < 0)
            {
                _selectedUnitIndex = _availableUnits.Count - 1;
            }
            else
            {
                _selectedUnitIndex = (_selectedUnitIndex - 1) % _availableUnits.Count;
            }
            
            ZombieUnit zombieUnit = _availableUnits[_selectedUnitIndex];
            if (!IsSelected(zombieUnit))
            {
                DeSelect();
                _selectedUnit = zombieUnit;
                zombieUnit.OnSelected();
                return true;
            }

            return false;
        }

        private void DeSelect()
        {
            if (_selectedUnit != null)
            {
                _selectedUnit.OnDeselected();
            }
        }

        private bool IsSelected(ZombieUnit zombieUnit) => _selectedUnit == zombieUnit;
    }
}