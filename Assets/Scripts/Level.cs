using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PlayerController player;

    public List<Transform> memberPrefabs;
    public int numberOfMembers;

    public List<SpawnerEnemy> spawnerEnemies = new List<SpawnerEnemy>();
    public int potionIterations = 1;
    public int waveValue;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Member> members;
    public List<Enemy> avoidList;
    public float bounds;
    public float spawnRadius;

    public int waveDuration;
    float spawnInterval;
    float spawnTimer;

    float waveTimer;

    // Start is called before the first frame update
    void Start()
    {
        members = new List<Member>();
        //enemies = new List<Enemy>();

        GenerateWave();
        avoidList.AddRange(FindObjectsOfType<Enemy>());
    }

    void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();

        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, spawnerEnemies.Count);
            int randEnemyCost = spawnerEnemies[randEnemyId].cost;

            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(spawnerEnemies[randEnemyId].prefab);
                waveValue -= spawnerEnemies[randEnemyId].cost;
            }
            else if(waveValue <= 0)
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                GameObject temp = Instantiate(enemiesToSpawn[0], new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
                members.Add(temp.GetComponent<Member>());
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = potionIterations * 5;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public List<Member> GetSameNeighbors(Member member, float radius)
    {
        // Get all Members inside a given radius
        // TODO: Make sure it's the same type of enemy that we're getting here! Bats should only follow bats, etc.

        List<Member> neighboursFound = new List<Member>();

        foreach (var item in members)
        {
            if(item == member)
            {
                continue;
            }

            if(Vector3.Distance(member.position, item.position) <= radius)
            {
                if (member.enemyType != item.enemyType) continue;
                //TODO: Check if other member is the same type of enemy!
                neighboursFound.Add(item);
            }
        }

        return neighboursFound;

    }

    public List<Member> GetAllNeighbors(Member member, float radius)
    {

        List<Member> neighboursFound = new List<Member>();

        foreach (var item in members)
        {
            if (item == member)
            {
                continue;
            }

            if (Vector3.Distance(member.position, item.position) <= radius)
            {
                neighboursFound.Add(item);
            }
        }

        return neighboursFound;

    }

    public List<Enemy> GetEnemies(Member member, float radius)
    {
        List<Enemy> returnEnemies = new List<Enemy>();

        foreach (var item in avoidList)
        {
            if(Vector3.Distance(member.position, item.position) <= radius)
            {
                returnEnemies.Add(item);
            }
        }
        return returnEnemies;
    }
}

[System.Serializable]
public class SpawnerEnemy
{
    public GameObject prefab;
    public int cost;
}
