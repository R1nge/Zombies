using UnityEngine;

namespace Game.Services
{
    public class MarkerPositionService
    {
        private readonly ConfigProvider _configProvider;
        private Transform _marker;

        private MarkerPositionService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void SetPosition(Vector3 position)
        {
            if (_marker == null)
            {
                _marker = Object.Instantiate(_configProvider.PositionMarkerConfig.Marker.transform);
            }
            
            _marker.gameObject.SetActive(true);

            _marker.position = position;
        }

        public void Hide()
        {
            if (_marker != null)
            {
                _marker.gameObject.SetActive(false);
            }
        }
    }
}