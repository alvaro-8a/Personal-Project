using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody enemyRb;
    protected GameObject player;

    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    protected float zBound = -12f;

    // POLYMORPHISM
    protected abstract void Move();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Deal Damage");
            DealDamage();
        }
    }

    private void DealDamage()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.healthPoints -= damage;
    }
}
