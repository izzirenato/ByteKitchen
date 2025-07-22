using System.Collections.Generic;
using UnityEngine;

public class UI_OptionButton : MonoBehaviour
{
    [Header("Oggetti da disattivare")]
    public List<GameObject> targets;

    // Funzione da collegare al bottone
    public void DisableColliders()
    {
        foreach (GameObject obj in targets)
        {
            if (obj != null)
            {
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
    }

    public void EnableColliders()
    {
        foreach (GameObject obj in targets)
        {
            if (obj != null)
            {
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    public void DisableMovement()
    {
        foreach (GameObject obj in targets)
        {
            if (obj != null)
            {
                Player movementScript = obj.GetComponent<Player>();
                if (movementScript != null)
                {
                    movementScript.enabled = false;
                }
            }
        }
    }

public void EnableMovement()
{
    foreach (GameObject obj in targets)
    {
        if (obj != null)
        {
            Player movementScript = obj.GetComponent<Player>();
            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
        }
    }
}



}
