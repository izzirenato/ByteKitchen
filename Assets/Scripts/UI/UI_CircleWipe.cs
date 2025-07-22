using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
public class UI_CircleWipe : MonoBehaviour
{
    public float duration = 1f; // Duration of the wipe effect in seconds

    RawImage _rawImage;
    Texture2D _maskTex;
    int _texW, _texH;

    void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _texW = (int)(Screen.width * 0.5);
        _texH = (int)(Screen.height * 0.5);
        _maskTex = new Texture2D(_texW, _texH, TextureFormat.RGBA32, false);
        _rawImage.texture = _maskTex;
    }

    private void Start()
    {
        FillMask(0f);
        StartCoroutine(AnimateWipe());
    }

    IEnumerator AnimateWipe()
    {
        yield return new WaitForSeconds(0.5f);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            FillMask(progress);
            yield return null;
        }
        FillMask(1f);
        _rawImage.raycastTarget = false;
    }

    void FillMask(float progress)
    {
        float maxRadius = Mathf.Sqrt(_texW * _texW + _texH * _texH) * 0.5f;
        float radius = Mathf.Lerp(0f, maxRadius, progress);

        Color32[] cols = new Color32[_texW * _texH];
        Vector2 center = new Vector2(_texW * 0.5f, _texH * 0.5f);

        for (int y = 0; y < _texH; y++)
        {
            for (int x = 0; x < _texW; x++)
            {
                int idx = x + y * _texW;
                float dist = Vector2.Distance(new Vector2(x, y), center);

                byte alpha = (byte)(dist < radius ? 0 : 255);
                cols[idx] = new Color32(0, 0, 0, alpha);
            }
        }

        _maskTex.SetPixels32(cols);
        _maskTex.Apply();
    }
}
