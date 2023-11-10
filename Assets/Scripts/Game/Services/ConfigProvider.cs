using Data;
using UnityEngine;

namespace Game.Services
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private PositionMarkerConfig positionMarkerConfig;
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private ZombiesUnitsConfig zombiesUnitsConfig;
        public PositionMarkerConfig PositionMarkerConfig => positionMarkerConfig;
        public UIConfig UIConfig => uiConfig;
        public ZombiesUnitsConfig ZombiesUnitsConfig => zombiesUnitsConfig;
    }
}