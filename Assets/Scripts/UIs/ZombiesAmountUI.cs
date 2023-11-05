﻿using TMPro;
using Units;
using UnityEngine;
using Zenject;

namespace UIs
{
    public class ZombiesAmountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI zombiesAmount;
        private UnitRTSController _unitRtsController;

        [Inject]
        private void Inject(UnitRTSController unitRtsController) => _unitRtsController = unitRtsController;
        private void Awake() => _unitRtsController.OnZombiesAmountChanged += UpdateUI;
        private void UpdateUI(int amount) => zombiesAmount.text = $"Zombies Alive: {amount}";
        private void OnDestroy() => _unitRtsController.OnZombiesAmountChanged -= UpdateUI;
    }
}