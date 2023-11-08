using System.Collections;
using System.Collections.Generic;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public abstract class Sensor : MonoBehaviour
    {
        [SerializeField] private float viewRadius;
        [SerializeField, Range(0, 360)] public float viewAngle;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;

        [SerializeField] private float meshResolution;
        [SerializeField] private int edgeResolveIterations;
        [SerializeField] private float edgeDstThreshold;
        [SerializeField] private MeshFilter viewMeshFilter;
        private Mesh viewMesh;
        private Transform _target;
        private readonly Collider[] _colliders = new Collider[2];


        public float Radius => viewRadius;

        private void Start()
        {
            viewMesh = new Mesh
            {
                name = "View Mesh"
            };

            viewMeshFilter.mesh = viewMesh;

            StartCoroutine(Sense_C());
        }

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
            int size = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, _colliders, targetMask);

            if (size != 0)
            {
                if (_colliders[0].TryGetComponent(out ZombieUnit zombieUnit))
                {
                    Transform target = _colliders[0].transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);
                        print("Detected a zombie");
                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                        {
                            print("Saw a zombie");
                            OnZombieSeen(zombieUnit);
                            _target = zombieUnit.transform;
                        }
                    }
                }
            }
        }

        protected abstract void OnZombieSeen(ZombieUnit zombieUnit);

        private void LateUpdate() => DrawFieldOfView();

        private void DrawFieldOfView()
        {
            int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
            float stepAngleSize = viewAngle / stepCount;
            List<Vector3> viewPoints = new List<Vector3>();
            ViewCastInfo oldViewCast = new ViewCastInfo();
            for (int i = 0; i <= stepCount; i++)
            {
                float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle);

                if (i > 0)
                {
                    bool edgeDstThresholdExceeded =
                        Mathf.Abs(oldViewCast.Distance - newViewCast.Distance) > edgeDstThreshold;
                    if (oldViewCast.Hit != newViewCast.Hit ||
                        (oldViewCast.Hit && edgeDstThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                        if (edge.PointA != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointA);
                        }

                        if (edge.PointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointB);
                        }
                    }
                }


                viewPoints.Add(newViewCast.Point);
                oldViewCast = newViewCast;
            }

            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 1) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            viewMesh.Clear();

            viewMesh.vertices = vertices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }

        private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            float minAngle = minViewCast.Angle;
            float maxAngle = maxViewCast.Angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;

            for (int i = 0; i < edgeResolveIterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(angle);

                bool edgeDstThresholdExceeded =
                    Mathf.Abs(minViewCast.Distance - newViewCast.Distance) > edgeDstThreshold;
                if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded ||
                    newViewCast.Hit != minViewCast.Hit && edgeDstThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.Point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.Point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }

        private ViewCastInfo ViewCast(float globalAngle)
        {
            Vector3 dir = DirFromAngle(globalAngle, true);

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewRadius, obstacleMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }

            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private struct ViewCastInfo
        {
            public readonly bool Hit;
            public readonly Vector3 Point;
            public readonly float Distance;
            public readonly float Angle;

            public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
            {
                Hit = hit;
                Point = point;
                Distance = distance;
                Angle = angle;
            }
        }

        private struct EdgeInfo
        {
            public readonly Vector3 PointA;
            public readonly Vector3 PointB;

            public EdgeInfo(Vector3 pointA, Vector3 pointB)
            {
                PointA = pointA;
                PointB = pointB;
            }
        }
    }
}