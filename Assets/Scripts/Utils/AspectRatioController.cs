using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    [SerializeField] private float targetAspect = 16.0f / 9.0f; // Rapporto target 16:9
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraRect();
    }

    void Update()
    {
        UpdateCameraRect();
    }

    void UpdateCameraRect()
    {
        // Calcola il rapporto corrente dello schermo
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Calcola il fattore di scala
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Lo schermo � pi� alto del target - aggiungi bande nere sopra e sotto
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            // Lo schermo � pi� largo del target - aggiungi bande nere ai lati
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}