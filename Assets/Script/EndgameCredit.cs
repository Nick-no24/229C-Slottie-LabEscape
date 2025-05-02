using UnityEngine;

public class EndGameCredit : MonoBehaviour
{
    public GameObject creditUI; // �ҡ Credit Panel �����
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Time.timeScale = 0f; // ��ش��
            creditUI.SetActive(true);
        }
    }
}