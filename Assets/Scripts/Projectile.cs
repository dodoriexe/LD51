using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float passedDownAttackDamage;
    float passedDownKnockback;
    float moveSpeed;
    public Vector2 directionToTravel;

    public bool justGoForwardDude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    void Travel()
    {
        if(justGoForwardDude)
        {
            transform.position += transform.up * (moveSpeed * 2) * Time.deltaTime;
        }
        else
        {
            transform.position += (Vector3)directionToTravel * (moveSpeed * 2) * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Entity enemy = collision.GetComponent<Entity>();

            enemy.TakeDamage(passedDownAttackDamage);
            enemy.TakeKnockback(this.gameObject, passedDownKnockback);
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(Vector2 dir, float damage, float speed, float knockback)
    {
        passedDownAttackDamage = damage;
        moveSpeed = speed;
        directionToTravel = dir;
        justGoForwardDude = false;
    }

    public void InitializeForward(float damage, float speed, float knockback)
    {
        passedDownAttackDamage = damage;
        moveSpeed = speed;
        justGoForwardDude = true;
    }
}
