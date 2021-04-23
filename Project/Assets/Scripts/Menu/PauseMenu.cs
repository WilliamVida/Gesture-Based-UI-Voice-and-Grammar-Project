using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

// From https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys.
public class PauseMenu : MonoBehaviour
{

    // Declare variables.
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    string mainMenuScene = "MainMenu";

    private GrammarRecognizer gr;
    private string phraseSpoken = "";
    public Text phraseSpokenText;

    // Initialise.
    void Start()
    {
        phraseSpokenText.text = "Spoken word(s):";
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "PauseMenuGrammar.xml"), ConfidenceLevel.Low);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running.");
    }

    // Check if escape if used.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                gr.Stop();
            }
            else
            {
                Pause();
                gr.Start();
            }
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
            phraseSpoken = valueString;
        }

        Debug.Log(message);
        phraseSpokenText.text = "Spoken word(s): " + phraseSpoken;
        PhraseRecogniser();
    }

    // Switch statement for the spoken phrase.
    private void PhraseRecogniser()
    {
        switch (phraseSpoken)
        {
            case "resume":
            case "resume the game":
                Resume();
                break;
            case "menu":
            case "to the menu":
            case "go to the menu":
            case "main menu":
            case "to the main menu":
            case "go to the main menu":
                LoadMenu();
                break;
            case "quit":
            case "quit the game":
            case "click the quit button":
            case "click quit":
                QuitGame();
                break;
        }
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

    // Method to pause the game.
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Method to resume the game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Method to load the main menu.
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    // Method to quit the game.
    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }

}
