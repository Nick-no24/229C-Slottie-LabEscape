using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float destroyDelay = 3f;

    void Start()
    {
        // ���¡����µ���ͧ��ѧ�ҡ���ҷ���˹�
        Destroy(gameObject, destroyDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // ����µ���ͧ���ⴹ�ѵ��
            Destroy(gameObject);

            // ����鴷� damage ������ ��
            // collision.collider.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }
}
