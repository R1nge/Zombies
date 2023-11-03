using UnityEngine;

namespace Data
{
    public abstract class UnitConfig : ScriptableObject
    {
        [SerializeField] private float speed;
        public float Speed => speed;
    }
}