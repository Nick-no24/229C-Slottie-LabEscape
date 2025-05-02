using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public Transform player;
    public Vector3 restartPosition;
    public MonoBehaviour playerControllerScript;
    public Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        if (playerControllerScript != null)
            playerControllerScript.enabled = false;


        player.position = restartPosition;
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }
        //deathUI.SetActive(false);


        Invoke(nameof(EnableControl), 0.1f);
    }
    void EnableControl()
    {
        if (playerControllerScript != null)
            playerControllerScript.enabled = true;
    }
}
