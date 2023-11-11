using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI Config")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private GameObject lostGameUI;
        [SerializeField] private GameObject wonGameUI;
        public GameObject InGameUI => inGameUI;
        public GameObject LostGameUI => lostGameUI;
        public GameObject WonGameUI => wonGameUI;
    }
}