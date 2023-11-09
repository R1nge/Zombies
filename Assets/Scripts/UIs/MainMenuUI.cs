using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIs
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button play;

        private void Awake() => play.onClick.AddListener(Play);

        private void Play() => SceneManager.LoadSceneAsync("Level1");

        private void OnDestroy() => play.onClick.RemoveAllListeners();
    }
}