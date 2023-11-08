using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Position Marker Config", menuName = "Configs/Position Marker Config")]
    public class PositionMarkerConfig : ScriptableObject
    {
        [SerializeField] private GameObject marker;
        public GameObject Marker => marker;
    }
}