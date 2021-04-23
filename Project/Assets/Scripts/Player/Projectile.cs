using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From https://www.youtube.com/watch?v=wkKsl1Mfp5M&ab_channel=Brackeys.
public class Projectile : MonoBehaviour
{

    // Declare variables.
    public float speed = 15.0f;
    public float damage = 20.0f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Set the transform to the right.
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // If the projectile hits an enemy then deal damage and destroy the projectile after two seconds.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            var effect = Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject.Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }

}
