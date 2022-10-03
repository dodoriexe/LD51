using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    Rigidbody2D rb;

    public Vector2 forceToApply;
    public float forceDamping;
    public PlayerCombat combat;
    private float second = 0f;

    public bool deathSlowmo;
    public PostProcessVolume deathProcessing;
    public GameObject scoreBoyPrefab;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        knockbackFeedback = GetComponent<KnockbackFeedback>();
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(deathSlowmo)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, .25f, 0.005f);
            deathProcessing.weight = Mathf.Lerp(deathProcessing.weight, 1, 0.005f);
            spriteRenderer.color = new Color(1, 0, 0, Mathf.Lerp(spriteRenderer.color.a, 0, 0.005f));

            if(deathProcessing.weight >= 0.95)
            {
                Scoreboy scoreBoy = Instantiate(scoreBoyPrefab).GetComponent<Scoreboy>();
                scoreBoy.numOfPotions = FindObjectOfType<PotionRoulette>().numOfIterations;
                SceneManager.LoadScene("GameOver");
            }

            return;
        }

        if (currentHP <= 0)
        {
            Die();
        }

        if (currentHP < maxHP)
        {
            second += Time.deltaTime;
            if(second >= healthRegenRate)
            {
                currentHP = Mathf.Min(currentHP + healthPerSecond, maxHP);
                second = 0f;
            }

            //if (currentHP > maxHP) currentHP = maxHP;
        }

        Vector2 PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(PlayerInput.x > 0.1f || PlayerInput.x < -0.1f || PlayerInput.y > 0.1f || PlayerInput.y < -0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if(Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        rb.velocity = moveForce;

        if(Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public new void Die()
    {
        deathSlowmo = true;
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

}
