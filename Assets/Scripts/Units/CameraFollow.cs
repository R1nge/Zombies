﻿using UnityEngine;

namespace Units
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;

        private void LateUpdate() => transform.position = target.position + offset;
    }
}