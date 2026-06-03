using UnityEngine;

public class HatProjectile : MonoBehaviour
{
    [SerializeField] private float returnDelay = 0.6f;
    [SerializeField] private float returnSpeed = 18f;

    private Transform owner;
    private Rigidbody2D rb;
    private bool returning = false;

    public void Init(Transform player)
    {
        owner = player;
        rb = GetComponent<Rigidbody2D>();

        Invoke(nameof(StartReturn), returnDelay);
    }

    private void StartReturn()
    {
        returning = true;
    }

    private void FixedUpdate()
    {
        if (!returning || owner == null) return;

        Vector2 direction = ((Vector2)owner.position - rb.position).normalized;
        rb.linearVelocity = direction * returnSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == owner)
        {
            Destroy(gameObject);
        }
    }
}