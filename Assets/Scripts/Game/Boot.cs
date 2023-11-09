using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Boot : MonoBehaviour
    {
        private void Start() => SceneManager.LoadSceneAsync("Main");
    }
}