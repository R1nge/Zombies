using System.Collections;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    [ExecuteAlways]
    public abstract class Sensor : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField, Range(0, 360)] public float angle;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstructionMask;
        private readonly Collider[] _colliders = new Collider[1];

        public float Radius => radius;

        private void Start() => StartCoroutine(Sense_C());

        private IEnumerator Sense_C()
        {
            YieldInstruction wait = new WaitForSeconds(.2f);

            while (enabled)
            {
                yield return wait;
                Sense();
            }
        }

        private void Sense()
        {
            int size = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, targetMask);

            if (size != 0)
            {
                if (_colliders[0].TryGetComponent(out ZombieUnit zombieUnit))
                {
                    if (!zombieUnit.enabled) return;

                    Transform target = _colliders[0].transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                            OnZombieSeen(zombieUnit);
                    }
                }
            }
        }

        protected abstract void OnZombieSeen(ZombieUnit zombieUnit);
    }
}