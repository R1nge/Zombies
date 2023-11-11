using UnityEngine;
using Zenject;

namespace Game.Services.Factories
{
    public class UIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ConfigProvider _configProvider;
        private GameObject _previousScreen;

        private UIFactory(DiContainer diContainer, ConfigProvider configProvider)
        {
            _diContainer = diContainer;
            _configProvider = configProvider;
        }
        
        public void CreateInGameUI()
        {
            _previousScreen = _diContainer.InstantiatePrefab(_configProvider.UIConfig.InGameUI);
        }

        public void CreateWonUI()
        {
            Object.Destroy(_previousScreen);
            _previousScreen = _diContainer.InstantiatePrefab(_configProvider.UIConfig.WonGameUI);
        }

        public void CreateLostUI()
        {
            Object.Destroy(_previousScreen);
            _previousScreen = _diContainer.InstantiatePrefab(_configProvider.UIConfig.LostGameUI);
        }
    }
}