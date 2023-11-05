using Factories;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] protected Animator animator;
        protected NavMeshAgent NavMeshAgent;
        protected UnitMovement UnitMovement;
        protected UnitAnimator UnitAnimator;
        protected CoroutineRunner CoroutineRunner;
        protected UnitFactory UnitFactory;

        [Inject]
        private void Inject(CoroutineRunner coroutineRunner, UnitFactory unitFactory)
        {
            CoroutineRunner = coroutineRunner;
            UnitFactory = unitFactory;
        }

        protected virtual void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            UnitMovement = new UnitMovement(NavMeshAgent);
            UnitAnimator = new UnitAnimator(animator);
        }

        protected abstract void Update();

        protected virtual void OnCollisionEnter(Collision collision)
        {
            print($"{gameObject.name} has collided with {collision.gameObject.name}");
        }

        public abstract void Idle();

        public abstract void StandUp();

        public abstract void Attack();

        public abstract void Die();
    }
}