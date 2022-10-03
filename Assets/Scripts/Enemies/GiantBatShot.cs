using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBatShot : MonoBehaviour
{
    GameObject player;
    GameObject projectile;
    bool onCooldown;

    public int numberOfShots;

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
        if (!onCooldown)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= 10f)
            {
                ShootRing();
            }
        }
    }

    void ShootRing()
    {
        float degree = 360f / numberOfShots;

        for (float i = -180f; i < 180f; i+= degree)
        {
            Quaternion rotation = Quaternion.AngleAxis(i, transform.forward);
            //Vector2 shotPos = transform.position + rotation;

            Transform bulletTransform = Instantiate(bulletPrefab, transform.position, rotation);

            bulletTransform.GetComponent<Projectile>().InitializeForward(entity.attackDamage, member.config.maxVelocity, entity.knockback);
        }
        StartCoroutine(BatShotCooldown());
    }

    IEnumerator BatShotCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(entity.attackSpeed);
        onCooldown = false;
    }
}
