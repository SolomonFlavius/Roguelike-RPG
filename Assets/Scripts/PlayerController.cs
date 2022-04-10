using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    public Animator anim;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;

    [HideInInspector]
    public bool canMove = true;

    public Gun theGun;

    private bool isMoving;

    private bool isDashing;

    private BoxCollider2D theBoxCollider;

    public int maxHealth = 5;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetHealthToPlayerHealthController();
        isDashing = false;
        theBoxCollider = GetComponent<BoxCollider2D>();

        activeMoveSpeed = moveSpeed;

        UIController.instance.currentGun.sprite = theGun.gunUI;
        UIController.instance.gunText.text = theGun.weaponName;
    }

    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            theRB.velocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }

            //rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    isDashing = true;
                    anim.SetTrigger("dash");

                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);

                    AudioManager.instance.PlaySFX(8);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    isDashing = false;
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
                isMoving = true;
            }
            else
            {
                anim.SetBool("isMoving", false);
                isMoving = false;
            }
        } else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            isMoving = false;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void SetHealthToPlayerHealthController()
    {
        PlayerHealthController.instance.maxHealth = maxHealth;
        PlayerHealthController.instance.HealPlayer(maxHealth);
    }
}
