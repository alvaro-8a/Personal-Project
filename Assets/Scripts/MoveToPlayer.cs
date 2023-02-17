using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public float speed = 300;

    private float zBound = -12f;

    private Rigidbody objectRb;
    private GameObject player;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if(gameObject.name.Contains("Monster 1"))
        {
            Debug.Log("Monster 1 Spawned");
            speed = spawnManager.monster1Speed;
        }
        else if(gameObject.name.Contains("Monster 2"))
        {
            Debug.Log("Monster 2 Spawned");
            speed = spawnManager.monster2Speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        objectRb.AddForce(direction * speed * Time.deltaTime);

        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }
}
