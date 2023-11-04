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
        private readonly UnitAnimator _unitAnimator;

        public HumanUnitTurningIntoZombie(CoroutineRunner coroutineRunner, ZombieUnit zombieUnit, UnitAnimator unitAnimator)
        {
            _coroutineRunner = coroutineRunner;
            _zombieUnit = zombieUnit;
            _unitAnimator = unitAnimator;
        }

        public void Enter()
        {
            _unitAnimator.ApplyRootMotion(true);
            _coroutineRunner.StartCoroutine(Wait());
            _zombieUnit.ChangeMesh();
            _zombieUnit.StandUp();
        }

        //TODO: use animator instead???
        //TODO: or create a config with animations and timings
        //TODO: USE TIMELINE
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(8f);
            _zombieUnit.enabled = true;
            _unitAnimator.ApplyRootMotion(false);
        }

        public void Update() { }

        public void Exit() { }
    }
}