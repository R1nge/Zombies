using UnityEngine;

namespace Units.Zombies
{
    public class ZombieDeathController
    {
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnit _zombieUnit;
        private readonly UnitMovement _unitMovement;

        public ZombieDeathController(UnitAnimator unitAnimator, ZombieUnit zombieUnit, UnitMovement unitMovement)
        {
            _unitAnimator = unitAnimator;
            _zombieUnit = zombieUnit;
            _unitMovement = unitMovement;
        }

        public void Die()
        {
            _unitMovement.Stop();
            _unitAnimator.ApplyRootMotion(true);
            _unitAnimator.PlayDeathAnimation();
            
            Component[] components = _zombieUnit.GetComponents<Component>();

            for (int i = components.Length - 1; i >= 0; i--)
            {
                if (components[i] is Transform transform) continue;
                Object.Destroy(components[i]);    
            }
        }
    }
}