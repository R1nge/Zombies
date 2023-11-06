using Data;
using Factories;
using Game.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitConfig unitConfig;
        [SerializeField] protected Animator animator;
        protected UnitMovement UnitMovement;
        protected UnitAnimator UnitAnimator;
        protected CoroutineRunner CoroutineRunner;
        protected UnitFactory UnitFactory;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _targetForward;

        [Inject]
        private void Inject(CoroutineRunner coroutineRunner, UnitFactory unitFactory)
        {
            CoroutineRunner = coroutineRunner;
            UnitFactory = unitFactory;
        }

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitConfig.Speed;
            UnitMovement = new UnitMovement(_navMeshAgent);
            UnitAnimator = new UnitAnimator(animator);
        }

        protected virtual void Update()
        {
            HandleSmoothForwardRotation();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            print($"{gameObject.name} has collided with {collision.gameObject.name}");
        }

        public abstract void Idle();

        public abstract void Die();

        public abstract bool CanBeAttackedBy(Unit unit);

        public void SetTargetForward(Vector3 targetForward) => _targetForward = targetForward;
        
        protected Vector3 DirectionFromPlayerTo(Transform target) => (transform.position - target.position).normalized;

        protected float Dot(Transform target) => Vector3.Dot(target.forward, DirectionFromPlayerTo(target));
        
        protected bool IsBehind(Transform target)
        {
            float dotOffset = .25f;
            return Dot(target) < -1 + dotOffset;
        }

        private void HandleSmoothForwardRotation()
        {
            if (_targetForward != Vector3.zero)
            {
                const float rotationSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, _targetForward, Time.deltaTime * rotationSpeed);

                if (transform.forward == _targetForward)
                {
                    _targetForward = Vector3.zero;
                }
            }
        }
    }
}