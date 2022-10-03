using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRat : MonoBehaviour
{
    PlayerController player;
    public LayerMask explosionLayers;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 1f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Member member = GetComponent<Member>();
        Entity entity = GetComponent<Entity>();

        member.config.maxVelocity = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3f, explosionLayers);

        foreach (var item in hitEnemies)
        {
            Entity enemyHit;

            if(item.GetComponent<PlayerController>())
            {
                enemyHit = item.GetComponent<PlayerController>();
            }
            else
            {
                enemyHit = item.GetComponent<Entity>();
            }

            enemyHit.TakeDamage(entity.attackDamage * 5);
            enemyHit.TakeKnockback(this.gameObject, entity.knockback * 5);
        }

        //TODO: Explosion

        entity.Die();

    }
}
