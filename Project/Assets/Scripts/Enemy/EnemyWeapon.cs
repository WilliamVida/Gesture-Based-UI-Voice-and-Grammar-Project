using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// form https://www.youtube.com/watch?v=kOzhE3_P2Mk&ab_channel=AlexanderZotov
public class EnemyWeapon : MonoBehaviour
{

    // Declare variables.
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float fireRate;
    float nextFire;

    // Set the next fire time.
    void Start()
    {
        nextFire = Time.time;
    }

    // Check if it is time to fire.
    void Update()
    {
        CheckIfTimeToFire();
    }

    // Method to check if the enemy can fire.
    private void CheckIfTimeToFire()
    {
        if (Time.time > nextFire && GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

}
