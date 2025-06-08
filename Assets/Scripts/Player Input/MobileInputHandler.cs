using System;
using UnityEngine;

public class MobileInputHandler : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        ToggleMobileInputs(false);  
#elif UNITY_ANDROID || UNITY_IOS
        ToggleMobileInputs(true);
#else
        ToggleMobileInputs(false);
#endif
    }

    private void ToggleMobileInputs(bool value)
    {
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}
