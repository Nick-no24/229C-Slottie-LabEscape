using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isOnLadder;
    private float moveInput;
    private float verticalInput;
    private bool canControl = true;

    [Header("Climbing")]
    public float climbSpeed = 4f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint2;
    public int maxAmmo = 5;
    private int currentAmmo;
    public float shootCooldown = 0.5f;
    private float shootTimer;
    public float reloadTime = 2f;
    private bool isReloading = false;

    [Header("UI")]
    public Text ammoText;
    public TMP_Text reloadTextTMP;
    public Slider playerHPBar;
   
    [Header("Health")]
    public int maxHealth = 100;
    public GameObject deathUI;
    private int currentHealth;
    private bool isDying = false;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;
    public Vector3 restartPosition = new Vector3(5f, 2f, 0f);
   


    public Transform player;
   

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentAmmo = maxAmmo;
        currentHealth = maxHealth;
       

        UpdateUI();
    }

    void Update()
    {
        if (!canControl ) return;

        moveInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.linearVelocity.y);

        if (moveInput != 0)
        {
            sprite.flipX = moveInput < 0;
            float fireX = Mathf.Abs(firePoint.localPosition.x);
            firePoint.localPosition = new Vector3(sprite.flipX ? -fireX : fireX, firePoint.localPosition.y, 0);
            firePoint2.localPosition = new Vector3(sprite.flipX ? -fireX : fireX, firePoint.localPosition.y, 0);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }

        if (isOnLadder)
        {
            rb2d.gravityScale = 0f;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, verticalInput * climbSpeed);
        }
        else
        {
            rb2d.gravityScale = 1f;
        }

        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.K) && shootTimer <= 0f && currentAmmo > 0)
        {
            Shoot();
        }

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (isDying) return;
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsJumping", !isGrounded && rb2d.linearVelocity.y > 0.1f);
        anim.SetBool("IsFalling", !isGrounded && rb2d.linearVelocity.y < -0.1f);
        anim.SetBool("IsClimbing", isOnLadder && Mathf.Abs(verticalInput) > 0.1f);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shoot);
        Vector2 shootDir = sprite.flipX ? Vector2.left : Vector2.right;
        bullet.GetComponent<Bullet>().SetDirection(shootDir);

        currentAmmo--;
        shootTimer = shootCooldown;
        UpdateUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadTextTMP.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            float timeLeft = Mathf.Max(0f, reloadTime - elapsed);
            reloadTextTMP.text = $"Reloading... {timeLeft:F1}s";
            yield return null;
        }

        currentAmmo = maxAmmo;
        isReloading = false;
        reloadTextTMP.gameObject.SetActive(false);
        UpdateUI();


    }

    void UpdateUI()
    {
        ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
        playerHPBar.value = (float)currentHealth / maxHealth;
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("CurrentHp" + currentHealth);
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            OnHit();
        }
    }

    public void OnHit(bool lockControlTemporarily = true)
    {
        anim.SetTrigger("HitTrigger");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);

        if (lockControlTemporarily)
        {
            StartCoroutine(DisableControlUntilAnimationEnds("Hurt-Animation"));
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        deathUI.SetActive(true);
    }

    IEnumerator DieAfterDelay(float delay, bool lockForever = false)
    {
        if (isDying) yield break;
        isDying = true;

        canControl = false;
        OnHit(false);

        yield return new WaitForSeconds(delay);
        Die();
    }

    IEnumerator DisableControlUntilAnimationEnds(string animationName)
    {
        canControl = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        canControl = true;
    }

    public void RespawnAtCheckpoint()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        isDying = false;
        canControl = true;

        anim.ResetTrigger("HitTrigger");
        anim.Play("Idle");

        transform.position = CheckpointManager.Instance.HasCheckpoint()
            ? CheckpointManager.Instance.GetCheckpoint()
            : Vector3.zero;

        deathUI.SetActive(false);
        UpdateUI();
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
        StartCoroutine(SetStartPositionAfterLoad());
    }

    IEnumerator SetStartPositionAfterLoad()
    {
        yield return null; 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform startPoint = GameObject.Find("StartPoint")?.transform;

        if (player != null && startPoint != null)
        {
            player.transform.position = startPoint.position;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }

        if (collision.CompareTag("DeathZone"))
        {
            Die();
        }

        if (collision.CompareTag("ThunderForce"))
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.AddForce(transform.position * -5, ForceMode2D.Impulse);
            StartCoroutine(DieAfterDelay(1, false));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }

        if (collision.CompareTag("Shroom") && rb2d.linearVelocity.y > 0.1f)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpShroom);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && !isDying)
        {
            StartCoroutine(DieAfterDelay(1.5f, true));
        }

        if (collision.CompareTag("Water"))
        {
            rb2d.gravityScale = 0.2f;
            if (Input.GetKey(KeyCode.W))
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 3f);
        }
    }
}
