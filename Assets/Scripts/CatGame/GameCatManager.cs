using UnityEngine;
using UnityEngine.UI;

public class GameCatManager : MonoBehaviour
{
    [Header("UI")]
    public Button playButton;
    public GameObject UIVictory;
    public Button replayButton;

    [Header("Game Logic")]
    public CatRandomPopUP catSpawner;



    private bool gameStarted = false;
    private int catClickCount = 0;
    private int maxClicks = 10;

    private void Start()
    {
        catSpawner.enabled = true;

        playButton.onClick.AddListener(StartMiniGame);
        replayButton.onClick.AddListener(StartMiniGame);
    }

    private void StartMiniGame()
{
    // Anche se è già partito, lo riavviamo da capo
    catClickCount = 0;
    gameStarted = true;

    catSpawner.StartGame();

    playButton.gameObject.SetActive(false);

    // Nasconde il pannello di vittoria se ancora attivo
    if (UIVictory != null)
        UIVictory.SetActive(false);
}


    public void CatClicked()
    {
        if (!gameStarted) return;

        catClickCount++;
        Debug.Log("Click: " + catClickCount);

        if (catClickCount >= maxClicks)
        {
            StopMiniGame();
        }
    }

    private void StopMiniGame()
{
    gameStarted = false;
    catSpawner.StopGame();

    Debug.Log("Gioco finito! Hai cliccato 10 gattini.");

    if (UIVictory != null)
        UIVictory.SetActive(true); // Rende visibile il pannello ogni volta
}

}

