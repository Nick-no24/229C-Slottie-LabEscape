using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D rb2D;
    public float lifeTime = 3f;
    public int damage = 1;
    public GameObject explosionEffect;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;

        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("OB"))
        {

            Debug.Log("Fireball hit: " + other.name + " | Tag: " + other.tag);
            Debug.Log("Hit: " + other.name + " | Tag: " + other.tag + " | Layer: " + LayerMask.LayerToName(other.gameObject.layer));
            if (other.CompareTag("Player"))
            {
               
                other.GetComponent<PlayerController>()?.TakeDamage(damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}