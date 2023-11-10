using UnityEngine;
using Zenject;

namespace Game.Services.Factories
{
    public class UIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ConfigProvider _configProvider;
        private GameObject _inGameUI;

        private UIFactory(DiContainer diContainer, ConfigProvider configProvider)
        {
            _diContainer = diContainer;
            _configProvider = configProvider;
        }
        
        public void CreateInGameUI()
        {
            _inGameUI = _diContainer.InstantiatePrefab(_configProvider.UIConfig.InGameUI);
        }   
    }
}