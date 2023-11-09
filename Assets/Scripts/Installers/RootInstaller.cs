using Game.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RootInstaller : MonoInstaller
    {
        [SerializeField] private ConfigProvider configProvider;
        [SerializeField] private CoroutineRunner coroutineRunner;

        public override void InstallBindings()
        {
            Container.BindInstance(configProvider);
            Container.BindInstance(coroutineRunner);
        }
    }
}