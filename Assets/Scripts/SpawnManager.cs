using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject powerup;
    public GameObject rock;

    // Spawn Pos restrictions
    private float ySpawn = 0.75f;
    private float zMonsterSpawn = 18.0f;
    private float xRange = 12.0f;
    private float zPowerupTop = 10.0f;
    private float zPowerupBottom = -3.0f;

    // Spawn timers
    private float startDelay = 1.0f;
    private float rockSpawnTimer = 1.5f;
    private float powerupSpawnTimer = 5.0f;

    // monster Waves
    public int monsterCount;
    public int waveCount = 2;

    public float monster1Speed = 2500f;
    public float monster2Speed = 2000f;
    public float speedIncrement = 200f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRock", startDelay, rockSpawnTimer);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;

        if (monsterCount == 0)
        {
            Debug.Log("Wave " + waveCount);

            SpawnMonsterWave(waveCount);
            waveCount += 2;
            monster1Speed += speedIncrement;
            monster2Speed += speedIncrement;
        }
    }

    // Instantiate the monster wave
    private void SpawnMonsterWave(int monstersToSpawn)
    {
        for (int i = 0; i < monstersToSpawn; i++)
        {
            int monsterIndex = Random.Range(0, monsters.Length);

            Instantiate(monsters[monsterIndex], GenerateSpawnPosition(), monsters[monsterIndex].gameObject.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        float randomX = RandomXPos();
        float randomZ = Random.Range(zPowerupBottom, zPowerupTop);
        Vector3 randomPos = new Vector3(randomX, ySpawn, randomZ);

        Instantiate(powerup, randomPos, powerup.transform.rotation);
    }

    private void SpawnRock()
    {
        float randomX = RandomXPos();
        Vector3 randomPos = new Vector3(randomX, ySpawn, zMonsterSpawn);

        Instantiate(rock, randomPos, rock.transform.rotation);
    }

    // Generate Random Spawn Positions for monsters
    private Vector3 GenerateSpawnPosition()
    {
        float randomX = RandomXPos();
        Vector3 randomPos = new Vector3(randomX, ySpawn, zMonsterSpawn);

        return randomPos;
    }

    // Random X Pos
    private float RandomXPos()
    {
        return Random.Range(-xRange, xRange);
    }
}
