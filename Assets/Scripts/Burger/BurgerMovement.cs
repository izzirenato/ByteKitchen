using UnityEngine;

public class BurgerMovement : MonoBehaviour
{
    [SerializeField] private Animator _burgerAnimator;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioSource _jumpSfx;
    [SerializeField] private AudioSource _dieSfx;

    // boolean flags to track the game state and the animation state
    private bool _isGameStarted = false;
    private bool _isGrounded = false;
    private bool _isDead = false;


    void Update()
    {
        bool isJumpButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        bool isCrouchButtonPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.DownArrow);

        if (_isGameStarted == true && !_isDead && isJumpButtonPressed && _isGrounded == true)
        {
            _jumpSfx.Play();
            Jump();
        }

        if(MinigameManager.Instance.gameStarted == true)
        {
            _isGameStarted = true;
        }


        // crouching logic is entirely managed by the crouch animation
        _burgerAnimator.SetBool("StartedGame", _isGameStarted);
        _burgerAnimator.SetBool("IsCrouching", isCrouchButtonPressed && _isGrounded);
        _burgerAnimator.SetBool("IsGrounded", _isGrounded);
        _burgerAnimator.SetBool("IsDead", _isDead);
    } 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isGrounded = true;
        }
        else
        {
            _dieSfx.Play();
            _isDead = true;
            MinigameManager.Instance.gameEnded = true;
            MinigameManager.Instance.ShowEndGameScreen();
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce);
        _isGrounded = false;
    }
}
