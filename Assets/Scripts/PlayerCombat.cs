using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerController playerController;
    float attackRange = 1f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public AudioSource punchNoise;

    CombatStyle combatStyle;

    [SerializeField]
    float additionalDamage;
    [SerializeField]
    public float currentFireCooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") > 0)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if(currentFireCooldown > 0) currentFireCooldown -= Time.fixedDeltaTime;
    }

    void Attack()
    {
        if(currentFireCooldown <= 0)
        {
            if (combatStyle == CombatStyle.MELEE)
            {
                //TODO: Animation

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (var item in hitEnemies)
                {
                    Debug.Log("We hit" + item.name);
                    Entity enemyHit = item.GetComponent<Entity>();

                    enemyHit.TakeDamage(playerController.attackDamage + additionalDamage);
                    enemyHit.TakeKnockback(this.gameObject, playerController.knockback);
                }

                if(hitEnemies.Length > 0)
                {
                    punchNoise.Play();
                }

            }

            else if (combatStyle == CombatStyle.RANGED)
            {

            }
            currentFireCooldown = playerController.attackSpeed;
        }
    }

    public void AddDamage(float damageToAdd)
    {
        additionalDamage = additionalDamage + damageToAdd;
    }
}

public enum CombatStyle
{
    MELEE,
    RANGED
}