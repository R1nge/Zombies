﻿using Units.Zombies;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class ZombieHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        private ZombieUnit _zombieUnit;

        private void Awake()
        {
            _zombieUnit = GetComponent<ZombieUnit>();
            _zombieUnit.ZombieHealth.OnHealthChanged += UpdateUI;
            _zombieUnit.ZombieHealth.OnMaxHealthChanged += UpdateMaxHealth;
        }

        private void UpdateMaxHealth(int maxHealth) => healthBar.maxValue = maxHealth;

        private void UpdateUI(int health)
        {
            healthBar.value = health;

            if (health == 0)
            {
                healthBar.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _zombieUnit.ZombieHealth.OnHealthChanged -= UpdateUI;
            _zombieUnit.ZombieHealth.OnMaxHealthChanged -= UpdateMaxHealth;
        }
    }
}