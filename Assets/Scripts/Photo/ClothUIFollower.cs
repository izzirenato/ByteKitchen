using UnityEngine;
using UnityEngine.UI;

public class ClothUIFollower : MonoBehaviour
{
    public RectTransform canvasRect;
    public RawImage cleaningImage;

    private Texture2D _cleaningTexture;
    private RectTransform _rectTransform;
    private Image _clothImage;

    private Camera _uiCamera;

    void Start()
    {
        _uiCamera = Object.FindFirstObjectByType<Camera>();
        _clothImage = GetComponent<Image>();
        _cleaningTexture = cleaningImage.texture as Texture2D;
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 localPos;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, _uiCamera, out localPos))
        {
            _clothImage.enabled = false;
            return;
        }

        Vector2 localPointCleaning;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cleaningImage.rectTransform, Input.mousePosition, _uiCamera, out localPointCleaning);
        Rect rect = cleaningImage.rectTransform.rect;

        float uvX = (localPointCleaning.x - rect.xMin) / rect.width;
        float uvY = (localPointCleaning.y - rect.yMin) / rect.height;

        if (uvX < 0 || uvX > 1 || uvY < 0 || uvY > 1)
        {
            _clothImage.enabled = false;
            return;
        }

        int px = Mathf.RoundToInt(uvX * _cleaningTexture.width);
        int py = Mathf.RoundToInt(uvY * _cleaningTexture.height);

        Color pixelColor = _cleaningTexture.GetPixel(px, py);

        if (pixelColor.a > 0.1f)
        {
            _clothImage.enabled = true;
            _rectTransform.localPosition = localPos;
        }
        else
        {
            _clothImage.enabled = false;
        }
    }
}