using Factories;
using Game.Services;
using Units;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;

        public override void InstallBindings()
        {
            Container.BindInstance(coroutineRunner);

            Container.Bind<HumanCounter>().AsSingle();
            Container.Bind<UnitRTSController>().AsSingle();

            Container.Bind<UnitFactory>().AsSingle();
        }
    }
}