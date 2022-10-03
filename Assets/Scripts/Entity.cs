using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KnockbackFeedback))]
public class Entity : MonoBehaviour
{
    public KnockbackFeedback knockbackFeedback;
    public Material flashMaterial;
    public Material originalMaterial;
    public Color originalColor;
    public float maxHP;
    public float currentHP;
    public float healthPerSecond; // aka. Life Regeneration
    public float healthRegenRate = 1f;
    public float moveSpeed;
    public float attackSpeed;
    public float baseAttackDamage;
    public float attackDamage;
    public float knockback;

    public AudioSource sauce;

    private float currentFireCooldown;

    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        PotionRoulette roulette = FindObjectOfType<PotionRoulette>();

        maxHP = maxHP + (0.8f * roulette.numOfIterations);
        attackDamage = attackDamage + (0.3f * roulette.numOfIterations);

        knockbackFeedback = GetComponent<KnockbackFeedback>();
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentHP <= 0)
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        if (currentFireCooldown > 0) currentFireCooldown -= Time.fixedDeltaTime;
    }

    public void Die()
    {
        FindObjectOfType<Level>().members.Remove(GetComponent<Member>());
        GameObject.Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        sauce.Play();
        Debug.Log("Entity " + name + " took " + damage + " damage!");
        currentHP -= damage;
        StartFlash();
    }

    public void TakeKnockback(GameObject sender, float strength)
    {
        knockbackFeedback.PlayFeedback(sender, strength);
    }

    public void StartFlash()
    {

        spriteRenderer.material = flashMaterial;
        spriteRenderer.color = Color.red;
        Invoke("StopFlash", 0.15f);
    }

    public void StopFlash()
    {
        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = originalColor;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentFireCooldown <= 0)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);
            collision.gameObject.GetComponent<PlayerController>().TakeKnockback(this.gameObject, knockback);

            currentFireCooldown = attackSpeed;
        }
    }
}
