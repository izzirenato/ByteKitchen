using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//da assegnare al giocatore:
//controlla il movimento tramite le freccette
//controlla le collisioni con gli oggetti interaggibili (per ora solo in cucina) --> frigo, credenza e robot
//cambia animazione in base alla direzione e al movimento
public class Player : MonoBehaviour
{
    //velocità di movimento
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 3;

    //i parametri collegati all'animator
    [Header("Animation Parameters")]
    [SerializeField] private string _horizontalDirectionParamName = "hDir";
    [SerializeField] private string _verticalDirectionParamName = "vDir";
    [SerializeField] private string _speedParamName = "isMoving";

    [Header("Other Settings")]
    [SerializeField] private GameObject _door;

    //parametri per collegare freccette e "E" per interagire con gli oggetti
    private InputAction _moveAction;
    private InputAction _interactAction;

    private Animator _anim;
    private Rigidbody2D _rb;

    private float _inputHorizontal = 0;
    private float _inputVertical = -1;
    private bool _isMoving = false;

    private LampControl _lampControl;
    private bool _hasInteractedWithLamp = false;
    private AudioSource _audioSource;

    //dizionario per gestire le interazioni con gli oggetti
    private readonly Dictionary<string, System.Action> _interactionHandlers = new();

    private void Awake()
    {
        InitializeComponents();
        InitializeInput();
        InitializeLampControl();
        SetupInteractionHandlers();

        if(_door == null)
        {
            Debug.Log("porta non assegnata");
        }else
        {
            _audioSource = _door.GetComponent<AudioSource>();
            if (_audioSource==null)
            {
                Debug.Log("audio source non collegato alla porta");
            }
        }
            
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mi riposiziona il giocatore nell'ultima posizione in cui era arrivato prima del cambio scena
        _rb.position = Consts.GameData.playerPosition;
    }

    void FixedUpdate()
    {
        //mi collega le freccette della tastiera a questa variabile Vector2 che chiamo move
        Vector2 move = _moveAction.ReadValue<Vector2>();

        //move.x e move.y sono float compresi tra -1 e 1

        if (move.x == 0 && move.y == 0) //se non si muove è in idle
        {
            _isMoving = false;
            UpdateSpeed(_isMoving);
        }
        else if (move.x != 0 && move.y != 0) //almeno uno deve essere uguale a zero, in questo modo sto evitando il movimento diagonale
        {
            //idle --> se il movimento è diagonale rimane fermo
            //Debug.Log("idle");
        }
        else //se il movimento non è diagonale cammina
        {
            //walk
            //assegno il movimento orizzontale e verticale per vedere in che direzione sto andando e cambiare l'animazione
            _inputHorizontal = move.x;
            _inputVertical = move.y;
            UpdateHorizontalDirection(_inputHorizontal);
            UpdateVerticalDirection(_inputVertical);

            _isMoving = true;
            UpdateSpeed(_isMoving); //aggiorno la variabile per cambiare l'animazione da idle a walk
            _rb.MovePosition(_rb.position + move * (_speed * Time.fixedDeltaTime)); //sposto fisicamente il GameObject
        }


    }

    private void InitializeComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

        if (_rb == null)
            Debug.LogError("Rigidbody2D component missing on Player!");
        if (_anim == null)
            Debug.LogError("Animator component missing on Player!");
    }

    private void InitializeInput()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _interactAction = InputSystem.actions.FindAction("Interact");

        if (_moveAction == null)
            Debug.LogError("Move action not found in Input System!");
        if (_interactAction == null)
            Debug.LogError("Interact action not found in Input System!");
    }

    private void InitializeLampControl()
    {
        var lampObject = GameObject.Find("Lamp");
        if (lampObject != null)
        {
            _lampControl = lampObject.GetComponent<LampControl>();
        }
    }

    private void SetupInteractionHandlers()
    {
        _interactionHandlers.Add("Fridge", HandleFridgeInteraction);
        _interactionHandlers.Add("Cupboard", HandleCupboardInteraction);
        _interactionHandlers.Add("Robot", HandleRobotInteraction);
        _interactionHandlers.Add("LivingRoom", HandleLivingRoomInteraction);
        _interactionHandlers.Add("Kitchen", HandleKitchenInteraction);
        _interactionHandlers.Add("Computer", HandleComputerInteraction);
        _interactionHandlers.Add("TurnTable", HandleTurnTableInteraction);
        _interactionHandlers.Add("Bookshelf", HandleBookshelfInteraction);
        _interactionHandlers.Add("Photo", HandlePhotoInteraction);
        _interactionHandlers.Add("Lamp", HandleLampInteraction);
        _interactionHandlers.Add("Cat", HandleCatInteraction);
    }


    public void UpdateHorizontalDirection(float dir)
    {
        _anim.SetFloat(_horizontalDirectionParamName, dir);
    }

    public void UpdateVerticalDirection(float dir)
    {
        _anim.SetFloat(_verticalDirectionParamName, dir);
    }

    public void UpdateSpeed(bool isMoving)
    {
        _anim.SetBool(_speedParamName, isMoving);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!_interactAction.IsPressed()) return;

        string tag = collision.collider.tag;
        if (_interactionHandlers.TryGetValue(tag, out var handler))
        {
            handler.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lamp"))
        {
            _hasInteractedWithLamp = false;
        }

        Debug.Log($"Interazioni totali: {GameData.interactionCount}");
    }


    private void HandleFridgeInteraction()
    {
        //se il robot non sta cucinando devo raccogliere gli ingredienti dal frigo
        if (!GameData.robotIsCooking)
        {
            LoadSceneAndSavePosition(SceneNames.Fridge);
        }
    }

    private void HandleCupboardInteraction()
    {
        // se il robot non sta cucinando devo raccogliere gli ingredienti dalla credenza
        if (!GameData.robotIsCooking)
        {
            LoadSceneAndSavePosition(SceneNames.Cupboard);
        }
    }

    private void HandleRobotInteraction()
    {
        if (!GameData.robotIsCooking) //se il robot non sta cucinando
        {
            if (GameData.recipe.AllIngredientsTaken()) //se ho preso tutti gli ingredienti che sono all'interno della ricetta
            {
                if (GameData.recipe.nameRecipe == Recipes.PASTA_PESTO)
                {
                    ShowMessage("non ci sono ingredienti da tagliare");
                    LoadSceneAndSavePosition(SceneNames.Mix); //carico la scena di mix
                }
                else
                {
                    ShowMessage("Taglio gli ingredienti...");
                    LoadSceneAndSavePosition(SceneNames.Cut); //carico la scena di taglio
                }
            }
            else
            {
                ShowMessage("Raccogli prima tutti gli ingredienti!");
            }
        }
        else if (!GameData.isMealReady) //se il robot sta cucinando ma il piatto non è pronto
        {
            ShowMessage("Sto cucinando, ripassa tra qualche minuto");
        }
        else // se il robot ha finito di cucinare e quindi il piato è pronto
        {
            ShowMessage("Ecco il piatto!");
            SceneManager.LoadScene(SceneNames.eat);
        }
    }

    private void HandleLivingRoomInteraction()
    {
        // se il robot sta cucinando, mi posso spostare in salotto
        if (GameData.robotIsCooking)
        {
            //SceneManager.LoadScene(SceneNames.Living);
            StartCoroutine(LoadSceneAfterSound(SceneNames.Living));
            GameData.playerPosition = new Vector2(5.45f, 1.4f);
        }
    }

    private void HandleKitchenInteraction()
    {
        // se il robot sta cucinando, mi posso spostare in cucina
        if (GameData.robotIsCooking)
        {
            //SceneManager.LoadScene(SceneNames.Kitchen);
            StartCoroutine(LoadSceneAfterSound(SceneNames.Kitchen));
            GameData.playerPosition = new Vector2(-3.66f, 2.51f);
        }
    }

    private void HandleComputerInteraction()
    {
        LoadSceneAndSavePosition(SceneNames.miniGame_1);
        IncrementInteractionCount("computer");
    }

    private void HandleTurnTableInteraction()
    {
        LoadSceneAndSavePosition(SceneNames.turnTable);
        IncrementInteractionCount("turntable");
    }

    private void HandleBookshelfInteraction()
    {
        LoadSceneAndSavePosition(SceneNames.bookshelf);
        IncrementInteractionCount("bookshelf");
    }

    private void HandlePhotoInteraction()
    {
        LoadSceneAndSavePosition(SceneNames.photo);
        IncrementInteractionCount("photo");
    }

    private void HandleLampInteraction()
    {
        if (!_hasInteractedWithLamp && _lampControl != null)
        {
            _hasInteractedWithLamp = true;
            _lampControl.UpdateLampState();
            GameData.playerPosition = _rb.position;
            IncrementInteractionCount("lamp");
        }
    }

    private void HandleCatInteraction()
    {
        LoadSceneAndSavePosition(SceneNames.miniGame_2);
        IncrementInteractionCount("cat");
    }

    private void LoadSceneAndSavePosition(string sceneName)
    {
        GameData.playerPosition = _rb.position;
        SceneManager.LoadScene(sceneName);
    }

    private void IncrementInteractionCount(string tag)
    {
        if (GameData.interactedObjects.Contains(tag))
        {
            ShowMessage("Hai gia' interagito con questo oggetto!");
            return;
        }
        else
        {
            GameData.interactedObjects.Add(tag);
            GameData.interactionCount++;
        }
        
    }

    private void ShowMessage(string message)
    {
        Debug.Log(message);
        // TODO: Implementare sistema UI per mostrare messaggi al giocatore
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }
}