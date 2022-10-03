using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatShots : MonoBehaviour
{

    GameObject player;
    bool onCooldown;

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
        Transform spawnerTransform = Instantiate(projectileSpawnerPrefab, transform.position, Quaternion.identity);
        spawnerTransform.GetComponent<ProjectileSpawner>().Initialize(entity, member);
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
