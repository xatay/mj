using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D _rb;
    private int currentPoint = 0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (patrolPoints.Length == 0)
            return;

        Vector2 targetPosition = patrolPoints[currentPoint].position;

        Vector2 newPos = Vector2.MoveTowards(
            _rb.position,
            targetPosition,
            speed * Time.fixedDeltaTime
        );

        _rb.MovePosition(newPos);

        if (Vector2.Distance(_rb.position, targetPosition) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}