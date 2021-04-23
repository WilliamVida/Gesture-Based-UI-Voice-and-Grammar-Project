using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From https://www.youtube.com/watch?v=wkKsl1Mfp5M&ab_channel=Brackeys
public class Enemy : MonoBehaviour
{

    // Declare variables.
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public AudioClip deathSound;
    AudioSource audioSource;
    [SerializeField] [Range(0f, 1.0f)] private float volume = 0.5f;
    public float maxHealth = 100f;
    public float health = 100.0f;
    public GameObject deathEffect;
    public HealthBar healthBar;

    // Set the health and audio source.
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
    }

    // If a projectile makes contact with the enemy then reduce health, set the health bar fill and call Die() if the health is less than or equal to zero.
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Set the death sounds and destroy the game object.
    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
        var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameObject.Destroy(effect, 2f);
        Destroy(gameObject);
    }

}
