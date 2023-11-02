using Units.States;
using UnityEngine;

namespace Units.Humans.States
{
    public class HumanUnitDead : IUnitState
    {
        public void Enter() { Debug.Log("HUMAN IS DEAD"); }

        public void Update() { }

        public void Exit() { }
    }
}