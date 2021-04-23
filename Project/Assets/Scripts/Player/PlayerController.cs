using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{

    // Declare variables.
    public GrammarRecognizer gr;
    public Text spokenText;

    private string phraseSpoken = "";
    private Rigidbody2D rb;

    private int defaultSpeed = 6;
    private int upperSpeed = 9;
    private int lowerSpeed = 3;
    int speed;

    private float xClamp = 7.5f;
    private float yClamp = 3.5f;
    float height = 3.0f;
    int direction = 1;

    public float maxHealth = 100f;
    public float health;
    public HealthBar healthBar;

    public PauseMenu pauseMenu;

    // Initialise.
    void Start()
    {
        Time.timeScale = 1f;
        speed = defaultSpeed;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Weapon weapon = gameObject.GetComponent<Weapon>();
        weapon.currentWeaponText.text = "Current Weapon: Fireball";
        rb = GetComponent<Rigidbody2D>();
        spokenText.text = "Spoken word(s):";
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "GameGrammar.xml"), ConfidenceLevel.Low);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running.");
    }

    // Move the player and check if the game is paused.
    void Update()
    {
        PlayerMovement();

        if (PauseMenu.gameIsPaused)
        {
            gr.Stop();
        }
        else
        {
            gr.Start();
        }
    }

    // Switch statement for the spoken phrase.
    private void PhraseRecogniser()
    {
        Weapon weapon = gameObject.GetComponent<Weapon>();

        switch (phraseSpoken)
        {
            case "weapon one":
            case "main weapon":
            case "fireball":
            case "primary weapon":
                weapon.selectedWeapon = 0;
                weapon.currentWeaponText.text = "Current Weapon: Fireball";
                break;
            case "weapon two":
            case "secondary weapon":
            case "purple pearl":
                weapon.selectedWeapon = 1;
                weapon.currentWeaponText.text = "Current Weapon: Purple Pearl";
                break;
            case "move":
                speed = defaultSpeed;
                break;
            case "normal speed":
            case "default speed":
                speed = defaultSpeed;
                break;
            case "move faster":
            case "go faster":
            case "faster":
            case "speed up":
                speed = upperSpeed;
                break;
            case "move slower":
            case "go slower":
            case "slower":
            case "speed down":
            case "slow down":
                speed = lowerSpeed;
                break;
            case "stop":
            case "stop moving":
                speed = 0;
                break;
            case "pause":
            case "pause the game":
                pauseMenu.Pause();
                break;
        }
    }

    // Check if a phrase is recognised.
    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        Debug.Log("Recognised a phrase.");
        SemanticMeaning[] meanings = args.semanticMeanings;

        // Use foreach to get all the meanings.
        foreach (SemanticMeaning meaning in meanings)
        {
            string valueString = meaning.values[0].Trim();
            message.Append("Value: " + valueString + " ");
            spokenText.text = "Spoken word(s): " + valueString;
            phraseSpoken = valueString;
        }

        Debug.Log(message);
        PhraseRecogniser();
    }

    // When the application quits.
    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }

    // Method for the player movement.
    private void PlayerMovement()
    {
        // https://forum.unity.com/threads/help-with-script-making-enemy-move-up-and-down.353603/
        if (transform.position.y >= yClamp)
            direction = -1;

        if (transform.position.y <= -yClamp)
            direction = 1;

        transform.Translate(0, speed * direction * Time.deltaTime, 0);
    }

    // Set the amount of fill in the health bar.
    void OnTriggerEnter2D(Collider2D col)
    {
        healthBar.SetHealth(health);

        if (health <= 0)
            Destroy(gameObject);
    }

}
