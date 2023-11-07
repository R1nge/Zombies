using System;
using UnityEngine;

namespace Units.Zombies
{
    public class ZombieHealth
    {
        public event Action<int, int> OnHealthChanged;
        private int _currentHealth;
        private readonly int _maxHealth;
        private readonly ZombieUnit _zombieUnit;

        public int MaxHealth => _maxHealth;

        public ZombieHealth(ZombieUnit zombieUnit, int maxHealth)
        {
            _zombieUnit = zombieUnit;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth == 0)
            {
                _zombieUnit.Die();
            }
        }
    }
}