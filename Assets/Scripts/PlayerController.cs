using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAmim;
    private Rigidbody playerRb;

    public float speed = 10f;

    private float topBound = 10.5f;
    private float bottomBound = 3.5f;
    private float xBound = 12.5f;
    private float dashDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAmim = GameObject.Find("Knight").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    // Move the player with arrow key input
    void MovePlayer()
    {
        float dashMultiplier = 1;

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // If user press LShift + dash delay has ellapsed
        // the player moves 10 times faster (Dash effect)
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashDelay <= 0)
        {
            dashMultiplier = 10f;
            dashDelay = 1.5f;
        }

        // Move player
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput * dashMultiplier);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput * dashMultiplier);

        if (verticalInput > 0)
        {
            playerAmim.SetFloat("Speed_f", 2.0f);
        }
        else  if(verticalInput < 0)
        {
            playerAmim.SetFloat("Speed_f", -2.0f);
        }
        else
        {
            playerAmim.SetFloat("Speed_f", 0.0f);
        }

        if (horizontalInput > 0)
        {
            playerAmim.SetFloat("Speed_l", 2.0f);
        }
        else if (horizontalInput < 0)
        {
            playerAmim.SetFloat("Speed_l", -2.0f);
        }
        else
        {
            playerAmim.SetFloat("Speed_l", 0.0f);
        }

        // Countdown the dash delay
        if(dashDelay > 0)
        {
            dashDelay -= Time.deltaTime;
        }
    }

    // Prevent player from leaving the screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }

        if (transform.position.z < -bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bottomBound);
        }

        if(transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Collision with Enemy or Rock");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
