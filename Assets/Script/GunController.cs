using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float timeToTarget = 1f;
    public GameObject targetMarker;
    private Vector2? targetPos = null;

    void Update()
    {
        // คลิกขวา -> ตั้งเป้า
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = mouseWorldPos;
            Debug.Log("Target Locked at: " + targetPos);

            if (targetMarker != null)
            {
                targetMarker.transform.position = mouseWorldPos;
                targetMarker.SetActive(true);
            }
        }

        // คลิกซ้าย -> ยิง
        if (Input.GetMouseButtonDown(0) && targetPos != null)
        {
            Shoot((Vector2)targetPos);
            targetPos = null;

            if (targetMarker != null)
                targetMarker.SetActive(false);
        }
    }

    void Shoot(Vector2 target)
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 velocity = CalculateProjectileVelocity(firePoint.position, target, timeToTarget);
        rb.linearVelocity = velocity;
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }
}
