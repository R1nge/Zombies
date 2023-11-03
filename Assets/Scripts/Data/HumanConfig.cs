using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "HumanConfig", menuName = "Configs/Human Configs")]
    public class HumanConfig : UnitConfig
    {
        [SerializeField] private Mesh humanMesh;
        public Mesh HumanMesh => humanMesh;
    }
}