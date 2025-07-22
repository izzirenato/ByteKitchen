using System.Collections;
using UnityEngine;

public class CameraZoom: MonoBehaviour
{
    public Transform target;            // L'oggetto su cui fare lo zoom
    public float zoomSize = 3f;         // Valore di zoom (orthographicSize)
    public float zoomDuration = 2f;        // Durata dello zoom in secondi
    public bool zoomOnStart = false;    // Zoom automatico all'avvio
    public GameObject _button;

    private float defaultSize; // Dimensione predefinita della camera
    private Vector3 defaultPosition; // Posizione predefinita della camera
    private bool isZoomed = false;

    void Awake()
    {
        defaultSize = Camera.main.orthographicSize;
        defaultPosition = Camera.main.transform.position;

        if (zoomOnStart && target != null)
        {
            ZoomToTarget();
        }

        if(_button == null)
        {
            Debug.LogError("Button non assegnato nel CameraZoom script.");
        }
        else
        {
            _button.SetActive(false); // Assicurati che il bottone sia inizialmente disattivato
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Premi Z per attivare/disattivare zoom
        {
            if (isZoomed)
                ResetZoom();
            else
                ZoomToTarget();
        }
    }

    public void ZoomToTarget()
    {
        if (target == null) return;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, Camera.main.transform.position.z);
        StopAllCoroutines();
        StartCoroutine(SmoothZoom(newPos, zoomSize));
        isZoomed = true;
    }

    public void ResetZoom()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothZoom(defaultPosition, defaultSize));
        isZoomed = false;
    }

    IEnumerator SmoothZoom(Vector3 targetPos, float targetSize)
    {
        float t = 0f;
        Vector3 startPos = Camera.main.transform.position;
        float startSize = Camera.main.orthographicSize;

        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float normalizedT = t / zoomDuration;

            Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, normalizedT);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, normalizedT);

            yield return null;
        }

        // Assicura che la camera arrivi esattamente al punto finale
        Camera.main.transform.position = targetPos;
        Camera.main.orthographicSize = targetSize;

        _button.SetActive(true); // Attiva o disattiva il bottone in base allo stato dello zoom
    }
}

