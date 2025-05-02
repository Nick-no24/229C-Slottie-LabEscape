using UnityEngine;

public class EndGameCredit : MonoBehaviour
{
    public GameObject creditUI; // ลาก Credit Panel มาใส่
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Time.timeScale = 0f; // หยุดเกม
            creditUI.SetActive(true);
        }
    }
}