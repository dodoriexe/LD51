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
    bool directional = false;

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
        directional = false;
    }

    public void InitializeDirection(Vector2 direction, Entity entity, Member member)
    {
        this.entity = entity;
        this.member = member;
        this.direction = direction;
        directional = true;
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
