using System.Collections.Generic;
using Units.Zombies;
using UnityEngine;

namespace Units
{
    public class UnitRTSController
    {
        private readonly HashSet<ZombieUnit> _selectedUnits = new();
        private readonly List<ZombieUnit> _availableUnits = new();

        public IReadOnlyCollection<ZombieUnit> SelectedUnits => _selectedUnits;
        public IReadOnlyList<ZombieUnit> AvailableUnits => _availableUnits;

        public void Add(ZombieUnit zombieUnit) => _availableUnits.Add(zombieUnit);

        public void Select(ZombieUnit zombieUnit)
        {
            if (!IsSelected(zombieUnit))
            {
                _selectedUnits.Add(zombieUnit);
                zombieUnit.OnSelected();
            }
        }

        public void DeSelect(ZombieUnit zombieUnit)
        {
            _selectedUnits.Remove(zombieUnit);
            zombieUnit.OnDeselected();
        }

        public void DeSelectAll()
        {
            foreach (ZombieUnit zombie in _selectedUnits)
            {
                zombie.OnDeselected();
            }

            _selectedUnits.Clear();
        }

        public bool IsSelected(ZombieUnit zombieUnit) => _selectedUnits.Contains(zombieUnit);
    }
}