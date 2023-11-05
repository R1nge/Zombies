﻿using Units.Humans;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(HumanSensor))]
    public class HumanSensorEditor : Editor
    {
        private void OnSceneGUI()
        {
            HumanSensor fov = (HumanSensor)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.Radius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.Radius);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}