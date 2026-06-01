using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 16f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isGrounded;
    private bool _jumpRequested = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        _jumpRequested = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
        HandleJump();
        HandleMovement();
        Debug.Log(_isGrounded);
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0f,
            whatIsGround
        );
    }

    private void HandleMovement()
    {
        float valueX = _moveInput.x * moveSpeed;
        _rb.linearVelocity = new Vector2(valueX, _rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (!_jumpRequested) return;
        _jumpRequested = false;
         
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(
            groundCheck.position,
            new Vector3(groundCheckSize.x, groundCheckSize.y, 0f)
        );
    }
}
