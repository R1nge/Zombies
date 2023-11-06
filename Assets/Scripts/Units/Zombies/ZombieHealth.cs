using System;
using UnityEngine;

namespace Units.Zombies
{
    public class ZombieHealth
    {
        public event Action<int> OnHealthChanged, OnMaxHealthChanged;
        private int _currentHealth;
        private readonly int _maxHealth;
        private readonly ZombieUnit _zombieUnit;

        public ZombieHealth(ZombieUnit zombieUnit, int maxHealth)
        {
            _zombieUnit = zombieUnit;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void Init()
        {
            OnMaxHealthChanged?.Invoke(_maxHealth);
            OnHealthChanged?.Invoke(_maxHealth);
        }

        public void TakeDamage(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);

            OnHealthChanged?.Invoke(_currentHealth);

            if (_currentHealth == 0)
            {
                _zombieUnit.Die();
            }
        }
    }
}