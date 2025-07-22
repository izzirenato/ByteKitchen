using UnityEngine;

public class ClickOnCat : MonoBehaviour
{
    public GameCatManager gameManager;
    public AudioClip meowSound; // Clip del miagolio da assegnare nell'Inspector

    private AudioSource audioSource;

    private float timer = 0f;
    private float lifetime = 2f;

    private float riseDuration = 0.5f;
    private float riseTimer = 0f;

    private bool rising = true;
    private bool falling = false;

    private Vector2 startPos;
    private Vector2 targetPos;

    private BoxCollider2D hitbox;

    private Vector3 basePosition;

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
        basePosition = transform.localPosition;

        // Non ci serve più l'audio source locale per il miagolio
        audioSource = GetComponent<AudioSource>();

    }

    private void OnEnable()
    {
        timer = 0f;
        riseTimer = 0f;
        rising = true;
        falling = false;

        startPos = new Vector2(basePosition.x, basePosition.y);
        targetPos = new Vector2(basePosition.x, basePosition.y + 2.32f);

        transform.localPosition = basePosition;

        if (hitbox != null)
            hitbox.enabled = false;
    }

    private void Update()
    {
        if (rising)
        {
            riseTimer += Time.deltaTime;
            float t = Mathf.Clamp01(riseTimer / riseDuration);

            float newY = Mathf.Lerp(startPos.y, targetPos.y, t);
            transform.localPosition = new Vector3(basePosition.x, newY, basePosition.z);

            if (t >= 1f)
            {
                rising = false;
                if (hitbox != null)
                    hitbox.enabled = true;
            }

            return;
        }

        if (falling)
        {
            riseTimer += Time.deltaTime;
            float t = Mathf.Clamp01(riseTimer / riseDuration);

            float newY = Mathf.Lerp(targetPos.y, startPos.y, t);
            transform.localPosition = new Vector3(basePosition.x, newY, basePosition.z);

            if (t >= 1f)
            {
                falling = false;
                gameObject.SetActive(false);
            }

            return;
        }

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            falling = true;
            riseTimer = 0f;

            if (hitbox != null)
                hitbox.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (!rising && !falling)
        {
            // Riproduce il miagolio da un oggetto temporaneo
            if (meowSound != null)
            {
                GameObject tempAudio = new GameObject("TempMeowAudio");
                tempAudio.transform.position = transform.position;
                
                TempAudioPlayer tempPlayer = tempAudio.AddComponent<TempAudioPlayer>();
                tempPlayer.PlayAndDestroy(meowSound, Consts.GameData.SFXvolume);
            }

            falling = true;
            riseTimer = 0f;

            if (hitbox != null)
                hitbox.enabled = false;

            gameManager.CatClicked();
        }
    }
}
