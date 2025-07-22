using UnityEngine;
using UnityEngine.Events;

public class Bookshelf : MonoBehaviour
{
    public Animator animator; // Assicurati di assegnare l'animator nel tuo inspector
    public AudioClip turnPageSound; // Assicurati di assegnare il suono di girata pagina nel tuo inspector
    public AudioSource audioSource; // Assicurati di assegnare l'AudioSource nel tuo inspector

    public UnityEvent onBookshelfClicked;
    public UnityEvent onBookRemoved;

    private void Awake()
    {
        if (onBookshelfClicked == null)
            Debug.LogWarning("onBookshelfClicked event is not assigned.");
        if (onBookRemoved == null)
            Debug.LogWarning("onBookRemoved event is not assigned.");

        if (animator == null)
        {
            Debug.LogError("Animator non assegnato");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource non assegnato");
        }

        if (turnPageSound == null)
        {
            Debug.LogError("TurnPageSound non assegnato");
        }
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                //Debug.Log("Hai cliccato su: " + clickedObject.name + " con tag: " + clickedObject.tag);

                if (clickedObject.CompareTag("Bookshelf"))
                {
                    //Debug.Log("Hai cliccato sulla libreria.");
                    onBookshelfClicked.Invoke();
                }
                else if (clickedObject.CompareTag("Book"))
                {
                    //Debug.Log("Hai girato pagina.");
                    animator.Play("turnPage", -1, 0f);
                    audioSource.PlayOneShot(turnPageSound);
                }
                else if (clickedObject.CompareTag("Background"))
                {
                    //Debug.Log("Hai rimosso il libro.");
                    onBookRemoved.Invoke();
                }
            }
        }
    }
}
