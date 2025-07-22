using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Consts;

public class ScratchEffect : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Setup")]
    public RawImage backgroundImage;
    public RawImage overlayImage;
    public int brushSize = 160;
    public Texture2D progressMask;
    public GameObject cloth;

    [Header("Progress")]
    public float targetPercentage = 80f;
    public Button livingButton;
    public GameObject robotMessage;

    private bool _targetReached = false;
    private Texture2D _overlayTexture;
    private bool _isScratching = false;
    private bool[] _maskPixelCache;
    private int _totalMaskPixels = 0;
    private Camera _uiCamera;

    [SerializeField] private AudioSource _audioPuliziaFoto;
    [SerializeField] private AudioSource _audioPuliziaFotoOk;


    private void Awake()
    {
        _uiCamera = Object.FindFirstObjectByType<Camera>();
        if (Consts.GameData.isPhotoClean)
        {
            cloth.gameObject.SetActive(false);
            robotMessage.SetActive(true);
            livingButton.gameObject.SetActive(true);
            overlayImage.gameObject.SetActive(false);
        }
        else 
        {
            if (livingButton == null)
            {
                Debug.Log("Bottone non collegato");
            }
            else
            {
                livingButton.gameObject.SetActive(false);
            }

            if (robotMessage == null)
            {
                Debug.Log("Messaggio robot non collegato");
            }
            else
            {
                robotMessage.SetActive(false);
            }
        }
        
    }

    void Start()
    {
        SetupScratchEffect();
        PrepareMaskCache();
    }

    void SetupScratchEffect()
    {
        // Crea una copia della texture overlay
        Texture2D originalTexture = overlayImage.texture as Texture2D;
        if (originalTexture != null)
        {
            _overlayTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
            _overlayTexture.SetPixels(originalTexture.GetPixels());
            _overlayTexture.Apply();

            overlayImage.texture = _overlayTexture;
        }
    }

    void PrepareMaskCache()
    {
        if (progressMask == null || _overlayTexture == null) return;

        Color[] maskPixels = progressMask.GetPixels();
        Color[] overlayPixels = _overlayTexture.GetPixels();

        _maskPixelCache = new bool[overlayPixels.Length];
        _totalMaskPixels = 0;

        for (int i = 0; i < maskPixels.Length && i < overlayPixels.Length; i++)
        {
            _maskPixelCache[i] = maskPixels[i].a > 0.1f;
            if (_maskPixelCache[i]) _totalMaskPixels++;
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        _isScratching = true;
        ScratchAtPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isScratching = false;
        StopPuliziaAudio();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isScratching)
        {
            ScratchAtPosition(eventData.position);
        }
    }

    void ScratchAtPosition(Vector2 screenPosition)
    {
        // Converti la posizione dello schermo in coordinate locali dell'immagine
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            overlayImage.rectTransform, screenPosition, _uiCamera, out localPoint);

        // Converti in coordinate UV (0-1)
        Rect rect = overlayImage.rectTransform.rect;
        float uvX = (localPoint.x - rect.xMin) / rect.width;
        float uvY = (localPoint.y - rect.yMin) / rect.height;

        // Controlla che sia dentro i limiti
        if (uvX >= 0 && uvX <= 1 && uvY >= 0 && uvY <= 1)
        {
            // Converti in coordinate pixel
            int pixelX = Mathf.RoundToInt(uvX * _overlayTexture.width);
            int pixelY = Mathf.RoundToInt(uvY * _overlayTexture.height);

            ScratchArea(pixelX, pixelY);   
        }
    }

    void ScratchArea(int centerX, int centerY)
    {
        if (_overlayTexture == null) return;

        int radius = brushSize / 2;
        bool textureModified = false;

        for (int x = centerX - radius; x <= centerX + radius; x++)
        {
            for (int y = centerY - radius; y <= centerY + radius; y++)
            {
                // Controlla i limiti della texture
                if (x >= 0 && x < _overlayTexture.width && y >= 0 && y < _overlayTexture.height)
                {
                    // Calcola la distanza dal centro per un effetto circolare
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));

                    if (distance <= radius)
                    {
                        // Rendi il pixel trasparente (grattato)
                        Color currentColor = _overlayTexture.GetPixel(x, y);

                        // Effetto graduale basato sulla distanza
                        float fadeAmount = 1f - (distance / radius);
                        currentColor.a = Mathf.Max(0, currentColor.a - fadeAmount);

                        _overlayTexture.SetPixel(x, y, currentColor);
                        textureModified = true;
                    }
                }
            }
        }

        // Applica i cambiamenti solo se abbiamo modificato dei pixel
        if (textureModified)
        {
            _overlayTexture.Apply();
            CheckProgress();

            if (!_audioPuliziaFoto.isPlaying)
            {
                _audioPuliziaFoto.volume = Consts.GameData.SFXvolume;
                _audioPuliziaFoto.Play();
            }
        }
    }

    [ContextMenu("Reset Scratch Effect")]
    public void ResetScratchEffect()
    {
        SetupScratchEffect();
        PrepareMaskCache();
    }

    public float GetScratchedPercentage()
    {
        if (_overlayTexture == null || progressMask == null || _totalMaskPixels == 0) return 0f;

        Color[] pixels = _overlayTexture.GetPixels();
        int scratchedMaskPixels = 0;

        for (int i = 0; i < pixels.Length; i++)
        {
            if (_maskPixelCache[i] && pixels[i].a < 0.1f)
                scratchedMaskPixels++;
        }

        return (float)scratchedMaskPixels / _totalMaskPixels * 100f;
    }

    void CheckProgress()
    {
        if (!_targetReached)
        {
            float currentPercentage = GetScratchedPercentage();
            if (currentPercentage >= targetPercentage)
            {
                Consts.GameData.isPhotoClean = true;
                _targetReached = true;
                _audioPuliziaFotoOk.volume = Consts.GameData.SFXvolume;
                _audioPuliziaFotoOk.Play();
                Debug.Log($"Target reached! {currentPercentage:F1}% scratched!");
                livingButton.gameObject.SetActive(true);
                robotMessage.SetActive(true);
            }
        }
    }

    void StopPuliziaAudio()
    {
        if (_audioPuliziaFoto.isPlaying)
            _audioPuliziaFoto.Stop();
    }

    void OnDestroy()
    {
        if (_overlayTexture != null)
        {
            DestroyImmediate(_overlayTexture);
        }
    }
}