using System.Collections;
using Units.States;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans.States
{
    public class HumanUnitTurningIntoZombie : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly ZombieUnit _zombieUnit;

        public HumanUnitTurningIntoZombie(CoroutineRunner coroutineRunner, ZombieUnit zombieUnit)
        {
            _coroutineRunner = coroutineRunner;
            _zombieUnit = zombieUnit;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(Wait());
            _zombieUnit.ChangeMesh();
            _zombieUnit.StandUp();
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(5f);
            _zombieUnit.enabled = true;
            //TODO: destroy human script???
        }

        public void Update() { }
        public void Exit() { }
    }
}