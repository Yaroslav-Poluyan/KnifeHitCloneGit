using UnityEngine;
using UnityEngine.UI;

public class OrientationCheck : MonoBehaviour
{

    private void Awake()
    {
        var isTabScreen = false;
        float aspect = (float) Screen.width / (float) Screen.height;
        if (aspect > 0.7f)
        {
            isTabScreen = true;
        }
        float match = isTabScreen ? 1f : 0f;
        var uiScalers = GameObject.FindObjectsOfType<CanvasScaler>();
        foreach (var scaler in uiScalers)
        {
            scaler.matchWidthOrHeight = match;
        }
    }
}
