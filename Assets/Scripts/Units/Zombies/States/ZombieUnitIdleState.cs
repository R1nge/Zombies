using System.Collections;
using Game.Services;
using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitIdleState : IUnitState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitMovement _unitMovement;
        private readonly ZombieHealth _zombieHealth;
        private bool _enabled;

        public ZombieUnitIdleState(CoroutineRunner coroutineRunner, UnitMovement unitMovement, ZombieHealth zombieHealth)
        {
            _coroutineRunner = coroutineRunner;
            _unitMovement = unitMovement;
            _zombieHealth = zombieHealth;
        }

        public void Enter()
        {
            _enabled = true;
            _coroutineRunner.StartCoroutine(ReduceHealthEverySecond_C());
            _unitMovement.Stop();
        }
        
        private IEnumerator ReduceHealthEverySecond_C()
        {
            while (_enabled)
            {
                yield return new WaitForSeconds(1f);
                _zombieHealth.TakeDamage(1);
            }
        }
        

        public void Update() { }

        public void Exit()
        {
            _enabled = false;
        }
    }
}