using UnityEngine;

namespace Units
{
    public class UnitSoundsController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void PlayFireSound() => audioSource.Play();
    }
}