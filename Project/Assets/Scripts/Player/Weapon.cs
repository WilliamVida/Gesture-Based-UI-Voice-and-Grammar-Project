using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

// From https://www.youtube.com/watch?v=wkKsl1Mfp5M&ab_channel=Brackeys.
public class Weapon : MonoBehaviour
{

    // Declare variables.
    [SerializeField] public AudioClip weaponSound;
    AudioSource audioSource;
    [SerializeField] [Range(0f, 1.0f)] private float volume = 0.5f;

    public Transform firePoint;
    public GameObject[] projectilePrefabs;
    public int selectedWeapon = 0;
    private bool canFire = true;
    public float weapon1FireRate = 0.4f;
    public float weapon2FireRate = 0.2f;
    public Text currentWeaponText;

    // Set the audio source.
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Check if the player can fire.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire)
        {
            StartCoroutine(Shoot());
        }
    }

    // Fire rate from https://answers.unity.com/comments/1720017/view.html.
    // Method to calculate if the player can shoot.
    IEnumerator Shoot()
    {
        canFire = false;
        audioSource.PlayOneShot(weaponSound, volume);
        var shot = Instantiate(projectilePrefabs[selectedWeapon], firePoint.position, firePoint.rotation);
        GameObject.Destroy(shot, 3.5f);

        if (selectedWeapon == 0)
            yield return new WaitForSeconds(weapon1FireRate);
        else if (selectedWeapon == 1)
            yield return new WaitForSeconds(weapon2FireRate);

        canFire = true;
    }

}
