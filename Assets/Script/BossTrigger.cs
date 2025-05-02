using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossController bossController;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            bossController?.ActivateBoss();
        }
    }
}
