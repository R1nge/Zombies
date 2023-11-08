using Data;
using UnityEngine;

namespace Game.Services
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private PositionMarkerConfig positionMarkerConfig;
        public PositionMarkerConfig PositionMarkerConfig => positionMarkerConfig;
    }
}