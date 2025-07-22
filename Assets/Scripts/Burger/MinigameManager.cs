using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [SerializeField] private MeshRenderer _floorMeshRenderer;
    [SerializeField] private AudioSource _pointSfx;

    [HideInInspector] public bool gameStarted = false;
    [HideInInspector] public bool gameEnded = false;


    [Header("Speed Settings")]
    public float startingSpeed = 0.3f;
    public float speedIncresePerSecond = 0.0001f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameEndScreen;
   

    [Header("Obstacle Spawn")]
    public float _minTimeDelayBetweenObstacleSpawns = 1f;
    public float _maxTimeDelayBetweenObstacleSpawns = 1f;
    public float _obstacleSpeedMultiplier = 3f;

    [Space]
    public GameObject[] allGroundObstacles;
    public GameObject ghost;

    [Space]
    public Transform groundObstacleSpawnPoint;
    public Transform ghostSpawnPoint;

    private List<GameObject> _allCurrentObstacles = new List<GameObject>();

    public float currentSpeed;
    private float _currentScoreIncreaseSpeedMultiplier = 2f;

    private int _highScore = 0;
    private float _currentScore = 0f;
    private float _timeUntilNextObstacle = 1f;
    private bool _highScoreNotification = false;    

    private void Awake()
    {
        Instance = this;
        gameEndScreen.SetActive(false);

        currentSpeed = startingSpeed;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            _highScore = PlayerPrefs.GetInt("HighScore");
        }
        UpdateScoreUI();
    }

    private void Update()
    {
        if(gameStarted && !gameEnded)
        {
            _timeUntilNextObstacle -= Time.deltaTime * currentSpeed;

            if (_timeUntilNextObstacle <= 0f)
            {
                _timeUntilNextObstacle = UnityEngine.Random.Range(_minTimeDelayBetweenObstacleSpawns, _maxTimeDelayBetweenObstacleSpawns);
                if (_currentScore >= 50)
                {
                    // randomly spawn ground or air obstacles
                    // 80% chance to spawn a ground obstacle, 20% chance to spawn a flying obstacle
                    if (UnityEngine.Random.value > 0.8f)
                    {
                        GameObject newObstacle = Instantiate(ghost, ghostSpawnPoint.position, Quaternion.identity);
                        _allCurrentObstacles.Add(newObstacle);
                    }
                    else 
                    {
                        GameObject newObstacle =  Instantiate(allGroundObstacles[UnityEngine.Random.Range(0, allGroundObstacles.Length)], 
                            groundObstacleSpawnPoint.position, Quaternion.identity);
                        _allCurrentObstacles.Add(newObstacle);
                    }
                }
                else
                {
                    GameObject newObstacle =  Instantiate(allGroundObstacles[UnityEngine.Random.Range(0, allGroundObstacles.Length)],
                            groundObstacleSpawnPoint.position, Quaternion.identity);
                    _allCurrentObstacles.Add(newObstacle);
                }
            }
            foreach (GameObject obstacle in _allCurrentObstacles.ToList())
            {
                obstacle.transform.Translate(Vector3.left * currentSpeed * Time.deltaTime * _obstacleSpeedMultiplier);
                if (obstacle.transform.position.x < -20f)
                {
                    Destroy(obstacle);
                    _allCurrentObstacles.Remove(obstacle);
                }
            }

            currentSpeed += Time.deltaTime * speedIncresePerSecond;
            _floorMeshRenderer.material.mainTextureOffset += new Vector2(currentSpeed * Time.deltaTime, 0f);
            _currentScore += currentSpeed * Time.deltaTime * _currentScoreIncreaseSpeedMultiplier;

            if (Mathf.RoundToInt(_currentScore) % 50 == 0 && Mathf.RoundToInt(_currentScore) != 0)
            {
                if (_pointSfx.isPlaying == false)
                {
                    _pointSfx.Play();
                }
            }

            if (Mathf.RoundToInt(_currentScore) > _highScore)
            {
                if (_highScoreNotification == false)
                {
                    if (_pointSfx.isPlaying == false)
                    {
                        _pointSfx.pitch += 1.5f;
                        _pointSfx.Play();
                        _pointSfx.pitch -= 1.5f;
                    }                   
                    _highScoreNotification = true;
                }
                _highScore = Mathf.RoundToInt(_currentScore);
                // key to store the high score in PlayerPrefs
                PlayerPrefs.SetInt("HighScore", _highScore);
            }
            UpdateScoreUI();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void ShowEndGameScreen()
    {
        gameEndScreen.SetActive(true);
    }

    private void UpdateScoreUI()
    {
        scoreText.SetText($"High Score: {_highScore.ToString("D5")}\n\nScore: {Mathf.RoundToInt(_currentScore).ToString("D5")}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Consts.SceneNames.miniGame_1);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(Consts.SceneNames.Living);
    }
}
