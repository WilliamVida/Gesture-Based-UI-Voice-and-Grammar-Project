using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{

    // Declare variables.
    public GameObject winnerMenu;
    public GameObject gameOverMenu;
    public WaveSpawner waveSpawner;

    // Check if the player exists or if there aren't any more waves to spawn.
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Time.timeScale = 0f;
            gameOverMenu.SetActive(true);
        }

        if (waveSpawner.finalWaveFinished)
        {
            Time.timeScale = 0f;
            winnerMenu.SetActive(true);
        }
    }

}
