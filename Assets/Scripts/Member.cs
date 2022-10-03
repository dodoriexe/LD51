using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public GameObject player;

    bool canMove = true;

    public Level level;
    public MemberConfig config;

    public EnemyType enemyType;

    private Vector3 wanderTarget;

    private Entity entity;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = gameObject.transform.localScale;

        level = FindObjectOfType<Level>();
        config = GetComponent<MemberConfig>();
        player = FindObjectOfType<PlayerController>().gameObject;

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

        entity = GetComponent<Entity>();
    }

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            acceleration = Combine();
            acceleration = Vector3.ClampMagnitude(acceleration, config.maxAcceleration);
            velocity = velocity + acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, config.maxVelocity);
            position = position + velocity * Time.deltaTime;

            transform.position = position;

            if(velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            else if(velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
    }

    public void UnableToMove()
    {
        canMove = false;
    }

    public void AbleToMove()
    {
        position = transform.position;
        canMove = true;
    }

    protected Vector3 Wander()
    {
        float jitter = config.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= config.wanderRadius;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(config.wanderDistance, config.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);

        // Steer towards target
        targetInWorldSpace -= this.position;

        return targetInWorldSpace.normalized;
    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbours = level.GetSameNeighbors(this, config.cohesionRadius);

        if(neighbours.Count == 0)
        {
            return cohesionVector;
        }

        foreach (var item in neighbours)
        {
            if(isInFOV(item.position))
            {
                cohesionVector += item.position;
                countMembers++;
            }
        }
        if(countMembers == 0)
        {
            return cohesionVector;
        }

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;

    }

    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = level.GetSameNeighbors(this, config.alignmentRadius);
        if(members.Count == 0)
        {
            return alignVector;
        }

        foreach (var item in members)
        {
            if(isInFOV(item.position))
            {
                alignVector += item.velocity;
            }
        }
        return alignVector.normalized;

    }

    Vector3 Separation()
    {
        Vector3 seperateVector = new Vector3();
        var members = level.GetAllNeighbors(this, config.seperationRadius);
        if(members.Count == 0)
        {
            return seperateVector;
        }

        foreach (var item in members)
        {
            if (isInFOV(item.position))
            {
                Vector3 movingTowards = this.position - item.position;
                if(movingTowards.magnitude > 0)
                {
                    seperateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }

        return seperateVector.normalized;
    }

    Vector3 Avoidance()
    {
        Vector3 avoidanceVector = new Vector3();
        var enemies = level.GetEnemies(this, config.avoidanceRadius);
        if(enemies.Count == 0)
        {
            return avoidanceVector;
        }

        foreach (var item in enemies)
        {
            avoidanceVector += RunAway(item.position);
        }

        return avoidanceVector.normalized;
    }

    Vector3 PlayerTracking()
    {
        Vector3 trackingVector = new Vector3();
        if (player.transform != null)
        {
            trackingVector -= Track(player.transform.position);
        }
        return trackingVector.normalized;
    }

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * config.maxVelocity;
        return neededVelocity - velocity;
    }

    Vector3 Track(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * config.maxVelocity;
        return neededVelocity + velocity;
    }

    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = config.cohesionPriority * Cohesion() + config.wanderPriority * Wander() +
            config.alignmentPriority * Alignment() + config.seperationPriority * Separation()
            + config.avoidancePriority * Avoidance() + config.trackingPriority * PlayerTracking();
        return finalVec;
    }

    bool isInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= config.maxFOV;
    }
}

public enum EnemyType
{
    NULL,
    BAT,
    RAT,
    EXPLOSIVERAT,
    DEMON
}