using UnityEngine;

public class FloatPlatFormMove : MonoBehaviour
{

    
    public Vector3 startLocalPos;
    public Vector3 endLocalPos;
    public float moveSpeed = 2f;
    public bool isActive;

    private Vector3 currentTarget;

    private void Start()
    {
        
        startLocalPos = transform.localPosition;
        currentTarget = endLocalPos;
    }

    private void Update()
    {
        if (!isActive) return;

        
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            currentTarget,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.localPosition, currentTarget) < 0.01f)
        {
            currentTarget = currentTarget == endLocalPos ? startLocalPos : endLocalPos;
        }
    }

    public void ActivatePlatform() => isActive = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isActive)
        {
            collision.transform.SetParent(transform); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isActive)
        {
            collision.transform.SetParent(null); 
        }
    }
}
