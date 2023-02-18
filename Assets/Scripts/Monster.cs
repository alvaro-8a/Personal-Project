using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Monster : Enemy
{
    private SpawnManager spawnManager;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (gameObject.name.Contains("Monster 1"))
        {
            Debug.Log("Monster 1 Spawned");
            speed = spawnManager.monster1Speed;
        }
        else if (gameObject.name.Contains("Monster 2"))
        {
            Debug.Log("Monster 2 Spawned");
            speed = spawnManager.monster2Speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        Move();
    }

    // POLYMORPHISM 
    protected override void Move()
    {
        transform.LookAt(player.transform);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(direction * speed * Time.deltaTime);

        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }

}
