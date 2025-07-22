using UnityEngine;
using System.Collections;

public class OvenController : MonoBehaviour
{
    public GameObject door;  
    public GameObject plate;
    public ParticleSystem steamEffect;


    private bool isOpen = false; // Variabile per sapere se il forno � gi� stato aperto

    
    public void OpenOven()
    {
        if (!isOpen) // Se il forno � ancora chiuso
        {
            StartCoroutine(OpenDoorSmooth()); // Avvia l'animazione per aprire lo sportello
            plate.SetActive(true);            // Mostra il piatto 
            isOpen = true;                    // Segna che il forno � stato aperto (evita riaperture)
        }
    }

    // Coroutine per aprire lo sportello lentamente
    private IEnumerator OpenDoorSmooth()
    {
        //  Attiva il vapore
        if (steamEffect != null)
        {
            steamEffect.Play();
        }

        float duration = 1.0f; // durata apertura
        float elapsed = 0f;    // tempo passato dall'inizio dell'animazione

        Quaternion startRot = door.transform.rotation;         // Rotazione iniziale dello sportello
        Quaternion endRot = Quaternion.Euler(90, 0, 0);        // Rotazione finale: apre verso il giocatore

        // Finch� il tempo passato � minore della durata desiderata...
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;             // Aggiunge il tempo del frame corrente
            float t = elapsed / duration;          // Percentuale dell'animazione completata (0 a 1)
            door.transform.rotation = Quaternion.Lerp(startRot, endRot, t); // Interpola la rotazione
            yield return null;                     // Aspetta il frame successivo
        }

        door.transform.rotation = endRot; 
    }
}
