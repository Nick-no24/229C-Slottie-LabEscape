using UnityEngine;

public class MonsterMove1 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public int damage = 10;

    private Transform currentTarget;
    private SpriteRenderer sprite;

    void Start()
    {
        currentTarget = pointB;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentTarget == null) return;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.05f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        if (sprite != null)
        {
            sprite.flipX = !sprite.flipX;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.TakeDamage(damage);
        }
    }
}
