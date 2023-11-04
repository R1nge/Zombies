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
            
            Container.Bind<UnitRTSController>().AsSingle();
        }
    }
}