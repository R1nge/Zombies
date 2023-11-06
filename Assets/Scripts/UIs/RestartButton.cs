using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIs
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button restart;

        private void Awake() => restart.onClick.AddListener(Restart);

        private void Restart() => SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);

        private void OnDestroy() => restart.onClick.RemoveAllListeners();
    }
}