using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBatShot : MonoBehaviour
{
    GameObject player;
    GameObject projectile;
    bool onCooldown;

    public int numberOfSpawners;

    public Transform projectileSpawnerPrefab;
    public Entity entity;
    public Member member;

    public float movementRecoveryDelay;

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
        float degree = 360f / numberOfSpawners;

        for (float i = -180f; i < 180f; i+= degree)
        {
            Quaternion rotation = Quaternion.AngleAxis(i, transform.forward);
            Transform spawnerTransform = Instantiate(projectileSpawnerPrefab, transform.position, rotation);
            spawnerTransform.GetComponent<ProjectileSpawner>().InitializeForward(entity, member);
        }
        StartCoroutine(BatShotMovementRecovery());
        StartCoroutine(BatShotCooldown());
    }

    IEnumerator BatShotMovementRecovery()
    {
        member.UnableToMove();
        yield return new WaitForSeconds(movementRecoveryDelay);
        member.AbleToMove();
    }

    IEnumerator BatShotCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(entity.attackSpeed);
        onCooldown = false;
    }
}
