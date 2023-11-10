using TMPro;
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
        private void Awake()
        {
            _unitRtsController.OnZombiesAmountChanged += UpdateUI;
            UpdateUI(0, _unitRtsController.AvailableUnitsCount);
        }

        private void UpdateUI(int previous, int amount) => zombiesAmount.text = $"Zombies Alive: {amount}";
        private void OnDestroy() => _unitRtsController.OnZombiesAmountChanged -= UpdateUI;
    }
}