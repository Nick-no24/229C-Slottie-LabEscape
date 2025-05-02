using UnityEngine;
using System.Collections;
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 5f;
    public float lifeTime = 3f;
    private Rigidbody2D rb2D;
    public Animator anim;

    public float destroyDelay = 0.3f;
    private Vector2 direction = Vector2.right;
    private bool hasHit = false;

    public BossController bossController;
    public void SetDirection(Vector2 dir)
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        rb2D.linearVelocity = dir.normalized * speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifeTime);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            hasHit = true;
            anim.SetTrigger("shootHit");
            Destroy(other.gameObject); 
            StartCoroutine(DestroyAfterAnim()); 
        }
         if (other.CompareTag("Boss"))
        {
            hasHit = true;
            BossController bossController = other.GetComponent<BossController>();
            if (bossController != null)
            {
                bossController.TakeDamage(damage);
            }

            anim.SetTrigger("shootHit");
            StartCoroutine(DestroyAfterAnim());
        }
        else if (!other.CompareTag("Player")&& !other.CompareTag("OB")) 
        {
            hasHit = true;
            anim.SetTrigger("shootHit");
            StartCoroutine(DestroyAfterAnim()); 
        }
        
    }
    private IEnumerator DestroyAfterAnim()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
