using Factories;
using Game.Services;
using Units;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private ConfigProvider configProvider;
        
        public override void InstallBindings()
        {
            Container.BindInstance(configProvider);
            Container.BindInstance(coroutineRunner);

            Container.Bind<MarkerPositionService>().AsSingle();
            Container.Bind<HumanCounter>().AsSingle();
            Container.Bind<UnitRTSController>().AsSingle();

            Container.Bind<UnitFactory>().AsSingle();
        }
    }
}