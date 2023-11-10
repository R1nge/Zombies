using Units.Humans.Military;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Military Units Config", menuName = "Configs/Military Units Config")]
    public class MilitaryUnitsConfig : ScriptableObject
    {
        [SerializeField] private MilitaryUnit militaryUnit;
        public MilitaryUnit MilitaryUnit => militaryUnit;
    }
}