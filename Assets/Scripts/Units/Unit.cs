using System;
using Data;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitConfig unitConfig;
        [SerializeField] protected Animator animator;
        protected NavMeshAgent NavMeshAgent;
        protected UnitMovement UnitMovement;

        protected virtual void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = unitConfig.Speed;

            UnitMovement = new UnitMovement(NavMeshAgent);
        }

        protected abstract void Update();

        protected virtual void OnCollisionEnter(Collision collision)
        {
            print($"{gameObject.name} has collided with {collision.gameObject.name}");
        }
    }
}