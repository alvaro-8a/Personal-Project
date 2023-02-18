using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Obstacles : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // ABSTRACTION
        Move();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        enemyRb.AddForce(Vector3.forward * -speed);

        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }
}
