using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    
    public float jumpForce,dashForce;

    private Rigidbody2D myRigibody;
    public BoxCollider2D playerCollider;

    public bool grounded = false;
    public bool doubleJumpAllowed = false;

    public LayerMask whatIsGround;

    private Collider2D myCollider;
    //public Animator anim;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public DeathMenu theDeathScreen;
    public PauseMenu pauseButton;
    [SerializeField] private TrailRenderer tr;
    private PlayerInput playerInput;
    private InputAction jump, dash, unDash;

    public float cooldownTime = 2;
    private float nextTime = 0;

    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();

        //anim = GetComponent<Animator>();

        speedMilestoneCount = speedIncreaseMilestone;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerInput = GetComponent<PlayerInput>();
        jump = playerInput.actions["Jump"];
        dash = playerInput.actions["Dash"];
        unDash = playerInput.actions["UnDash"];
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (myRigibody.velocity.y == 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (grounded)
        {
            doubleJumpAllowed = true;
        }
        if (grounded && jump.triggered)
        {
            Jump();
        }
        else if (doubleJumpAllowed && jump.triggered)
        {
            Jump();
            doubleJumpAllowed = false;
        }

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }
        myRigibody.velocity = new Vector2(moveSpeed, myRigibody.velocity.y);
        if (dash.triggered && canDash)
        {
            StartCoroutine(Dash());
        }

    }
    public void DoubleJump()
    {
        
    }
    private void Jump()
    {
            myRigibody.velocity = new Vector2(myRigibody.velocity.x, jumpForce);
            myRigibody.AddForce(Vector2.up * jumpForce);   
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = myRigibody.gravityScale;
        myRigibody.gravityScale = 0f;
        myRigibody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        myRigibody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time > nextTime)
        {
            if (collision.gameObject.tag == "Enemy")
            {

				switch (collision.GetComponent<Enemies>().enemyType)
				{
                    case Enemies.EnemyType.UNBREAK:
                        TakeDamage(20);
                        break;
                    case Enemies.EnemyType.BREAKABLE:
						if (isDashing)
						{
                            Destroy(collision.gameObject);
                        }
						else
						{
                            TakeDamage(20);
                        }
                        break;
				}
                
                
                if (currentHealth <= 0)
                {
                    theDeathScreen.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    pauseButton.gameObject.SetActive(false);
                    //buttonController.gameObject.SetActive(false);
                }
                nextTime = Time.time + cooldownTime;
            }
        }
        if (collision.gameObject.tag == "Fall")
        {
            TakeDamage(100);
            theDeathScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
            pauseButton.gameObject.SetActive(false);
            //buttonController.gameObject.SetActive(false);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
