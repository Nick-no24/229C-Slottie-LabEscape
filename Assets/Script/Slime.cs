using UnityEngine;

public class Slime : MonoBehaviour
{
    public Transform[] waypoints;   // จุดที่ต้องการให้เดินผ่าน
    public float moveSpeed = 2f;
    public float reachThreshold = 0.1f;
    public int damage = 10;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // ถ้าใกล้จุดเป้าหมาย → ไปจุดถัดไป
        if (Vector3.Distance(transform.position, target.position) < reachThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }


}
