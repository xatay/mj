using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Hat")]
    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 12f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnThrow(InputValue value)
    {
        if (!value.isPressed) return;
        ThrowHat();
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }

    private void ThrowHat()
    {
        GameObject hat = Instantiate(hatPrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = hat.GetComponent<Rigidbody2D>();

        Vector2 direction = Vector2.right;

        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        HatProjectile hatScript = hat.GetComponent<HatProjectile>();
        hatScript.Init(transform);
    }

    

}
