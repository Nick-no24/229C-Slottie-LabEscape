using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float destroyDelay = 3f;

    void Start()
    {
        // เรียกทำลายตัวเองหลังจากเวลาที่กำหนด
        Destroy(gameObject, destroyDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // ทำลายตัวเองถ้าโดนศัตรู
            Destroy(gameObject);

            // ใส่โค้ดทำ damage ได้ที่นี่ เช่น
            // collision.collider.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }
}
