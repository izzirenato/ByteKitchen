using UnityEngine;
using UnityEngine.Events;

public class VinylMusicManager : MonoBehaviour
{
    public UnityEvent activePanelVinyls;
    public UnityEvent hidePanelVinyls;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Controlla se il pulsante sinistro è stato premuto
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null) // Se il raggio colpisce un Collider2D
            {
                if(hit.collider.CompareTag("Vinyl"))
                {
                    activePanelVinyls?.Invoke();
                }
                else if (hit.collider.CompareTag("Button"))
                {
                    hidePanelVinyls?.Invoke();
                    Debug.Log("Pulsante premuto: " + hit.collider.name);
                }
            }
        }
    }
}
