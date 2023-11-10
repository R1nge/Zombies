using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI Config")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject inGameUI;
        public GameObject InGameUI => inGameUI;
    }
}