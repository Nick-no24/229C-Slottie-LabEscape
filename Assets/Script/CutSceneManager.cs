using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Transform character;
    public Transform walkTarget;
    public float walkSpeed = 2f;
    public GameObject breakePoint;
    public GameObject GameUi;

    public Rigidbody2D characterRb;
    public MonoBehaviour playerControllerScript;



    private bool walking = true;
    private bool falling = false;

    void Start()
    {
        
        characterRb.gravityScale = 0f;

        
    }

    void Update()
    {
        if (walking)
        {
            character.position = Vector3.MoveTowards(character.position, walkTarget.position, walkSpeed * Time.deltaTime);

            if (Vector3.Distance(character.position, walkTarget.position) < 0.1f)
            {
                walking = false;
                breakePoint.SetActive(false);
              GameUi.SetActive(true);
                playerControllerScript.enabled = true;
                
            }
        }
        
    }

   
   
}
