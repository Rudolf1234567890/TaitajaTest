using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class resolution_script : MonoBehaviour
{
    public int targetWidth = 360;
    public int targetHeight = 640;

    private void Start()
    {
        FitToAspect();
    }

    void FitToAspect()
    {
        RawImage rawImage = GetComponent<RawImage>();
        RectTransform rt = rawImage.rectTransform;

        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = (float)targetWidth / targetHeight;

        if (screenAspect >= targetAspect)
        {
            // Fit height, pad width
            float width = targetAspect / screenAspect;
            rt.anchorMin = new Vector2(0.5f - width / 2, 0);
            rt.anchorMax = new Vector2(0.5f + width / 2, 1);
        }
        else
        {
            // Fit width, pad height
            float height = screenAspect / targetAspect;
            rt.anchorMin = new Vector2(0, 0.5f - height / 2);
            rt.anchorMax = new Vector2(1, 0.5f + height / 2);
        }

        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }
}
