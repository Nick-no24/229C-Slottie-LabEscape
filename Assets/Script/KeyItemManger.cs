using UnityEngine;

public class KeyItemManager : MonoBehaviour
{
    public static KeyItemManager instance;
    private int gemCount = 0;
    public int gemRequired = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectGem()
    {
        gemCount++;
        Debug.Log("Collected gem: " + gemCount);

        if (gemCount >= gemRequired)
        {
            Debug.Log("All gems collected!");
            
        }
    }

    public int GetGemCount() => gemCount;

}
