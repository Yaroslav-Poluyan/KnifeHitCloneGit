using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);

    void Start()
    {
        Panel = GetComponent<RectTransform>();
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = GetSafeArea();

        if (safeArea != LastSafeArea)
            ApplySafeArea(safeArea);
    }

    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;

        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        #if UNITY_EDITOR
        if (Screen.width == 1242 && Screen.height == 2688)
        {
            anchorMax.y = 0.96f;
        }
        #endif
        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;
    }
}