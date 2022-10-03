using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    GameObject player;
    Entity entity;
    Member member;
    int numShots;
    bool canShoot = false;
    bool forward = false;

    public Transform bulletPrefab;
    public int totalShots;
    public float delay;
    public float cooldown;
    public Vector2 direction;
    public bool shootTowardsPlayer;

    public void Initialize(Entity entity, Member member)
    {
        this.entity = entity;
        this.member = member;
        forward = false;
    }

    public void InitializeForward(Entity entity, Member member)
    {
        this.entity = entity;
        this.member = member;
        forward = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(WaitDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            if (numShots < totalShots)
            {
                Shoot();
                canShoot = false;
                StartCoroutine(WaitCooldown());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Shoot()
    {
        Transform bulletTransform = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        if(shootTowardsPlayer)
            direction = (player.transform.position - transform.position).normalized;

        if(forward)
            bulletTransform.GetComponent<Projectile>().InitializeForward(entity.attackDamage, member.config.maxVelocity, entity.knockback);
        else
            bulletTransform.GetComponent<Projectile>().Initialize(direction, entity.attackDamage, member.config.maxVelocity, entity.knockback);

        numShots++;
    }

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
