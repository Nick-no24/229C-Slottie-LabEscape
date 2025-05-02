using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject pressEnterUI;
    public GameObject cutsceneManager;
    public static StartGame Instance;
    public Vector3? forcedStartPosition = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.startgame);
            hasStarted = true;
            pressEnterUI.SetActive(false); // ปิด UI กด Enter
            cutsceneManager.SetActive(true); // เริ่ม Cutscene
        }
    }
}
