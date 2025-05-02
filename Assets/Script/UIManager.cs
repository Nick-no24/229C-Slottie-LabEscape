using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image[] gemIcons;         // 3 Image �ͤ͹
    public Sprite gemCollectedIcon;  // �ٻ gem ���������
    private int gemCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddGem()
    {
        if (gemCount < gemIcons.Length)
        {
            gemIcons[gemCount].sprite = gemCollectedIcon;
            gemCount++;
        }

        if (gemCount >= gemIcons.Length)
        {
           
        }
    }
}
