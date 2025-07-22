using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Tooltip("Parallax speed multiplier (0 = stationary, 1 = same speed as game elements)")]
    [Range(0f, 2f)]
    public float parallaxSpeedMultiplier = 0.5f;

    private MeshRenderer meshRenderer;

    // Reference to the current game speed from MinigameManager
    private float currentGameSpeed => MinigameManager.Instance != null ? MinigameManager.Instance.currentSpeed : 0f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError($"ParallaxBackground on {gameObject.name} requires a MeshRenderer component!");
        }
    }

    private void Update()
    {

        if (MinigameManager.Instance == null ||
            !MinigameManager.Instance.gameStarted ||
            MinigameManager.Instance.gameEnded ||
            meshRenderer == null)
            return;

        // Only move if the game manager exists, game has started, and we have a valid renderer
        if (MinigameManager.Instance == null || !MinigameManager.Instance.gameStarted || meshRenderer == null)
            return;

        float offsetMovement = currentGameSpeed * parallaxSpeedMultiplier * Time.deltaTime;
        meshRenderer.material.mainTextureOffset += Vector2.right * offsetMovement;
    }
}