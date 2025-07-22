using UnityEngine;

public class CatRandomPopUP : MonoBehaviour
{
    public Transform catPositionsParent; // Genitore dei 3 gatti

    private GameObject[] catObjects;
    private GameObject currentCat;
    private int lastIndex = -1;

    private bool isGameActive = false;

    private void Start()
    {
        int count = catPositionsParent.childCount;
        catObjects = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            catObjects[i] = catPositionsParent.GetChild(i).gameObject;
            catObjects[i].SetActive(false); // nasconde tutti i gatti all'avvio
        }

        // SpawnNewCat() verrà chiamato solo dopo StartGame()
    }

    private void Update()
    {
        if (!isGameActive) return;

        if (currentCat == null || !currentCat.activeSelf)
        {
            SpawnNewCat();
        }
    }

    void SpawnNewCat()
    {
        int newIndex;

        do
        {
            newIndex = Random.Range(0, catObjects.Length);
        } while (newIndex == lastIndex && catObjects.Length > 1);

        lastIndex = newIndex;
        currentCat = catObjects[newIndex];
        currentCat.SetActive(true);
    }

    public void StartGame()
    {
        isGameActive = true;
    }

    public void StopGame()
    {
        isGameActive = false;

        // Disattiva tutti i gatti rimasti attivi
        foreach (var cat in catObjects)
        {
            cat.SetActive(false);
        }
}

}