using UnityEngine;

public class SetFps : MonoBehaviour
{
    private void Start()
    {
        #if UNITY_ANDROID
        Application.targetFrameRate = 61;
        #endif
        
        #if UNITY_WEBGL
        Application.targetFrameRate = 61;
        #endif
        
#if DEVELOPMENT_BUILD
Debug.logger.logEnabled=true;
#else
        Debug.unityLogger.logEnabled=true;
#endif

    }
}