using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatShots : MonoBehaviour
{

    GameObject player;
    GameObject projectile;
    bool onCooldown;

    public Transform bulletPrefab;
    public Entity entity;
    public Member member;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!onCooldown)
        {
            if(Vector2.Distance(transform.position, player.transform.position) <= 10f)
            {
                ShootAtPlayer();
            }
        }
    }

    void ShootAtPlayer()
    {

        Transform bulletTransform = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector2 shootDirection = (player.transform.position - transform.position).normalized;
        bulletTransform.GetComponent<Projectile>().Initialize(shootDirection, entity.attackDamage, member.config.maxVelocity, entity.knockback);
        StartCoroutine(BatShotCooldown());
    }

    IEnumerator BatShotCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(entity.attackSpeed);
        onCooldown = false;
    }
}
