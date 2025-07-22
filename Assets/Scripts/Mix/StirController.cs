using UnityEngine;
using UnityEngine.UI;

public class StirController : MonoBehaviour
{    public Transform pivot;
    public Transform fluido;
    public float rotationSpeed = 80f;

    public SpriteRenderer fluidoSprite;
    public Sprite fluidoStart;
    public Sprite fluidoIdle;
    public Sprite fluidoMixing;
    public Sprite fluidoFinal;

    [SerializeField] private GameObject braccio;
    public Button button;
    public Collider2D stirHitbox;
    public GameObject message;
    public GameObject guide;

    [SerializeField] private AudioSource stirringAudio; // AudioSource per il mescolamento

    private Animator animator;
    private float holdTime = 0f;
    private bool _mixingCompleted = false;
    private AudioSource _audioSource;


    void Awake()
    {
        if (braccio != null)
        {
            animator = braccio.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 0f;
            }
            else
            {
                Debug.LogError("Animator non trovato nel braccio!");
            }
        }
        else
        {
            Debug.LogError("Braccio non assegnato!");
        }

        if (button != null)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Bottone non collegato");
        }

        if (stirHitbox == null)
        {
            Debug.LogWarning("Aggiungi Stir Hitbox (Boxcollider2d)");
        }


        if (fluidoSprite == null)
        {
            Debug.LogWarning("Aggiungi Fluido Sprite Renderer");
        }

        if (fluidoStart == null)
        {
            Debug.LogWarning("Aggiungi Fluido Start Sprite");
        }

        if (fluidoIdle == null)
        {
            Debug.LogWarning("Aggiungi Fluido Idle Sprite");
        }

        if (fluidoMixing == null)
        {
            Debug.LogWarning("Aggiungi Fluido Mixing Sprite");
        }

        if (fluidoFinal == null)
        {
            Debug.LogWarning("Aggiungi Fluido Final Sprite");
        }

        if (message != null)
        {
            message.SetActive(false);
            _audioSource = message.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("Messaggio non assegnato!");
        }

        if (guide != null)
        {
            guide.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Guida non assegnata!");
        }
        
         if (stirringAudio == null)
        {
            Debug.LogWarning("AudioSource per il mescolamento non assegnato!");
        }
    }

    void Start()
    {
        Color color = UpdateColorSprite();

        fluidoSprite.color = color;

        if (fluidoSprite != null && fluidoStart)
            fluidoSprite.sprite = fluidoStart;
    }

   void Update()
    {
        if (_mixingCompleted) return;

        bool isPressing = false;

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (stirHitbox != null && stirHitbox.OverlapPoint(mousePos))
            {
                isPressing = true;
                holdTime += Time.deltaTime;

                if (holdTime >= 6f)
                {
                    _mixingCompleted = true;
                    fluidoSprite.sprite = fluidoFinal;
                    guide.SetActive(false);
                    button.gameObject.SetActive(true);
                    message.SetActive(true);
                    _audioSource.Play();
                    animator.speed = 0f;

                    if (stirringAudio != null && stirringAudio.isPlaying)
                        stirringAudio.Stop();

                    return;
                }
            }
        }

        // Gestione audio in base attivo
        if (stirringAudio != null)
        {
            if (isPressing && !stirringAudio.isPlaying)
            {
                stirringAudio.Play();
            }
            else if (!isPressing && stirringAudio.isPlaying)
            {
                stirringAudio.Stop();
            }
        }

        // Cambia sprite fluido
        if (fluidoSprite != null)
        {
            if (isPressing)
            {
                fluidoSprite.sprite = fluidoMixing;
            }
            else if (holdTime > 0f)
            {
                fluidoSprite.sprite = fluidoIdle;
            }
        }

        // Rotazioni
        if (isPressing)
        {
            pivot.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            if (fluido != null)
                fluido.Rotate(0f, 0f, rotationSpeed * 0.5f * Time.deltaTime);
        }

        // Animazione braccio
        if (animator != null)
        {
            animator.speed = isPressing ? 1f : 0f;
        }
    }

    public Color UpdateColorSprite()
    {
        switch (Consts.GameData.recipe.nameRecipe)
        {
            case Consts.Recipes.TORTA_CIOCCOLATO:
                return new Color(0.2f, 0.1f, 0.1f, 1f); // Marrone scuro
            case Consts.Recipes.COUS_COUS_ZUCCHINE:
                return new Color(0.5f, 0.8f, 0.5f, 1f); // Verde chiaro
            case Consts.Recipes.BROWNIE:
                return new Color(0.3f, 0.2f, 0.1f, 1f); // Marrone medio
            case Consts.Recipes.PIZZA_MARGHERITA:
                return new Color(0.8f, 0.7f, 0.4f, 1f); // Beige chiaro
            case Consts.Recipes.TORTA_MELE:
                return new Color(0.8f, 0.7f, 0.4f, 1f); // Beige chiaro
            case Consts.Recipes.SFORMATO_ZUCCA_PATATE:
                return new Color(0.9f, 0.6f, 0.3f, 1f); // Arancione chiaro
            case Consts.Recipes.CHEESECAKE_FRUTTI_BOSCO:
                return new Color(0.9f, 0.8f, 0.9f, 1f); // Rosa chiaro
            case Consts.Recipes.TAGLIATA:
                return new Color(0.6f, 0.3f, 0.2f, 1f); // Rosso scuro
            case Consts.Recipes.PANCAKE_BANANA:
                return new Color(0.9f, 0.8f, 0.5f, 1f); // Giallo chiaro
            case Consts.Recipes.PASTA_PESTO:
                return new Color(0.5f, 0.8f, 0.5f, 1f); // Verde chiaro
            case Consts.Recipes.ROTOLO_ARANCIA:
                return new Color(0.9f, 0.6f, 0.3f, 1f); // Arancione chiaro
            case Consts.Recipes.PASTA_FUNGHI_SALSICCIA:
                return new Color(0.6f, 0.4f, 0.3f, 1f); // Marrone chiaro
            case Consts.Recipes.BISCOTTI_MANDORLE:
                return new Color(0.6f, 0.4f, 0.3f, 1f); //Marrone chiaro
            case Consts.Recipes.FRITTATA_ZUCCHINE:
                return new Color(0.5f, 0.8f, 0.5f, 1f); // Verde chiaro
            case Consts.Recipes.MUFFIN_CIOCCOLATO:
                return new Color(0.2f, 0.1f, 0.1f, 1f); // Marrone scuro
            case Consts.Recipes.PATATE_FORNO:
                return new Color(0.9f, 0.8f, 0.5f, 1f); // Giallo chiaro
        }

        return new Color(1f, 1f, 1f, 1f); // Bianco di default
    }
}
