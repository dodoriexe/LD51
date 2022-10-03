using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStab : MonoBehaviour
{

    Member member;
    Entity entity;

    public LayerMask playerLayer;
    public GameObject hitPoint;
    public Animator animator;

    float normalVelocity;
    bool onCooldown;

    // Start is called before the first frame update
    void Start()
    {
        member = gameObject.GetComponent<Member>();
        entity = gameObject.GetComponent<Entity>();

        onCooldown = false;

        normalVelocity = member.config.maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!onCooldown)
        {
            if (collision.CompareTag("PlayerSide"))
            {
                Stab();
            }
        }
    }

    public void Stab()
    {
        if(!onCooldown)
        {
            member.config.maxVelocity = .1f;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.transform.position, 3f, playerLayer);
            animator.Play("Demon_Stab");

            foreach (var item in hitEnemies)
            {
                PlayerController enemyHit = item.GetComponent<PlayerController>();

                if (enemyHit == null) continue;

                Debug.Log(enemyHit.name);

                enemyHit.TakeDamage(entity.attackDamage * 3);
                enemyHit.TakeKnockback(gameObject, entity.knockback * 7.5f);
            }

            StartCoroutine(StabCooldown());
        }
    }

    IEnumerator StabCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(2f);
        ResumeMoving();
    }

    public void ResumeMoving()
    {
        member.config.maxVelocity = normalVelocity;
        onCooldown = false;
    }
}
