using UnityEngine;

public class SetFps : MonoBehaviour
{
    private void Start()
    {
        #if UNITY_ANDROID
        Application.targetFrameRate = 31;
        #endif
        
        #if UNITY_WEBGL
        Application.targetFrameRate = 61;
        #endif
    }
}