using System;
using Units.Zombies;
using Unity.AI.Navigation;
using UnityEngine;

namespace Game.Interactables
{
    public class BreakableWall : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out ZombieUnit zombieUnit))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                FindObjectOfType<NavMeshSurface>().BuildNavMesh();
                gameObject.SetActive(false);
            }
        }
    }
}