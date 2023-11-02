using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] private float speed;
        public float Speed => speed;
    }
}