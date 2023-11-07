using System;
using System.Collections.Generic;
using Units.Zombies;

namespace Units
{
    public class UnitRTSController
    {
        public event Action<int> OnZombiesAmountChanged;
        private readonly List<ZombieUnit> _availableUnits = new();
        private ZombieUnit _selectedUnit;
        private int _selectedUnitIndex;

        public ZombieUnit SelectedUnit => _selectedUnit;

        public void Add(ZombieUnit zombieUnit)
        {
            _availableUnits.Add(zombieUnit); 
            OnZombiesAmountChanged?.Invoke(_availableUnits.Count);
        }

        public void Remove(ZombieUnit zombieUnit)
        {
            DeSelect();
            _availableUnits.Remove(zombieUnit); 
            OnZombiesAmountChanged?.Invoke(_availableUnits.Count);
        }

        public void SelectNext()
        {
            _selectedUnitIndex = (_selectedUnitIndex + 1) % _availableUnits.Count;
            ZombieUnit zombieUnit = _availableUnits[_selectedUnitIndex];
            if (!IsSelected(zombieUnit))
            {
                DeSelect();
                _selectedUnit = zombieUnit;
                zombieUnit.OnSelected();
            }
        }

        public void SelectPrevious()
        {
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
            }
        }

        public void DeSelect()
        {
            if (_selectedUnit != null)
            {
                _selectedUnit.OnDeselected();
            }
        }

        public bool IsSelected(ZombieUnit zombieUnit) => _selectedUnit == zombieUnit;
    }
}