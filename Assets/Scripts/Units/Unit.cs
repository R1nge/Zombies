using UnityEngine;
using UnityEngine.AI;

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

        protected virtual void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            UnitMovement = new UnitMovement(NavMeshAgent);
            UnitAnimator = new UnitAnimator(animator);

            CoroutineRunner = FindObjectOfType<CoroutineRunner>();
        }

        protected abstract void Update();

        protected virtual void OnCollisionEnter(Collision collision)
        {
            print($"{gameObject.name} has collided with {collision.gameObject.name}");
        }

        public abstract void StandUp();

        public abstract void Attack();

        public abstract void Die();
    }
}