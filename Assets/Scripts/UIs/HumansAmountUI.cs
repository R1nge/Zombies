using Game.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace UIs
{
    public class HumansAmountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI humansAmount;
        private HumanCounter _humanCounter;

        [Inject]
        private void Inject(HumanCounter humanCounter) => _humanCounter = humanCounter;
        private void Awake()
        {
            _humanCounter.OnHumanCountChanged += UpdateUI;
            UpdateUI(_humanCounter.HumanCount);
        }

        private void UpdateUI(int amount) => humansAmount.text = $"Humans Alive: {amount}";
        private void OnDestroy() => _humanCounter.OnHumanCountChanged -= UpdateUI;
    }
}