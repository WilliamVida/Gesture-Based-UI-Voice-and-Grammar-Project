using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// form https://www.youtube.com/watch?v=kOzhE3_P2Mk&ab_channel=AlexanderZotov
public class EnemyProjectile : MonoBehaviour
{

    // Declare variables.
    public float speed = 8f;
    Rigidbody2D rb;
    PlayerController target;
    Vector2 direction;
    public float damage = 20f;

    // Initialise.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        direction = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(direction.x, direction.y);
        Destroy(gameObject, 6f);
    }

    // If the enemy projectile hits the player then decrease the health;
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController playerController = col.GetComponent<PlayerController>();

        if (col.gameObject.tag == ("Player"))
        {
            playerController.health -= damage;
            Destroy(gameObject);
        }
    }

}
