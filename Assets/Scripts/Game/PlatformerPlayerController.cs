using Consts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//da assegnare al giocatore, controlla il movimento tramite le freccette e le collisioni con gli oggetti interaggibili (per ora solo in cucina) caricando le apposite scene
public class PlatformerPlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 4;

    private InputAction _moveAction;
    private InputAction _interactAction;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        //_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //if(_spriteRenderer == null)
        //{
        //    Debug.Log("Non ho trovato lo sprite renderer");
        //}
        //Sprite mioSprite = Resources.Load<Sprite>("Square");
        //if (mioSprite != null)
        //{
        //    _spriteRenderer.sprite = mioSprite;
        //}
        //else
        //    Debug.Log("non ho trovato nessuno sprite");
        //non funziona


            _moveAction = InputSystem.actions.FindAction("Move");
            _interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb.position = Consts.GameData.playerPosition; //adesso mi si bugga perchè continua a richiamare oncolliderenter
    }

    void FixedUpdate()
    {
        Vector2 move = _moveAction.ReadValue<Vector2>(); //mi collega le freccette della tastiera a questa variabile Vector2 che chiamo move

        //il vettore move ha 2 componenti comprese tra -1 e 1
        //Debug.Log("direzione orizzontale " + move.x);
        //Debug.Log("direzione verticale " + move.y);

        //non so come fare per ruotare il personaggio
        //float angle = 
        //_rb.MoveRotation();

        if(move.x !=0 && move.y !=0)
        {
            //idle
            Debug.Log("idle");
        }
        else
        {
            _rb.MovePosition(_rb.position + move * (_speed * Time.fixedDeltaTime));
        }

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fridge"))
        {
            if (_interactAction.IsPressed())
            {
                SceneManager.LoadScene(Consts.SceneNames.Fridge);
                Consts.GameData.playerPosition = _rb.position;
            }

        }

        if (collision.collider.CompareTag("Cupboard"))
        {
            if (_interactAction.IsPressed())
            {
                SceneManager.LoadScene(Consts.SceneNames.Cupboard);
                Consts.GameData.playerPosition = _rb.position;
            }

        }

        if (collision.collider.CompareTag("Robot"))
        {
            if (_interactAction.IsPressed())
            {
                if(GameData.recipe.AllIngredientsTaken())
                {
                    SceneManager.LoadScene(Consts.SceneNames.Cut);
                    //Consts.GameData.playerPosition = _rb.position;
                }
                
            }

        }
    }
}
