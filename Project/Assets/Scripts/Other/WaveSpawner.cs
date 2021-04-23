using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// From https://www.youtube.com/watch?v=Vrld13ypX_I&ab_channel=Brackeys.
// From https://www.youtube.com/watch?v=q0SBfDFn2Bs&ab_channel=Brackeys.
public class WaveSpawner : MonoBehaviour
{

    // Declare variables.
    public enum SpawnState { Spawning, Waiting, Counting };

    // Declare class for the wave.
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }
    public Text waveText;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;
    public SpawnState State
    {
        get { return state; }
    }
    public bool finalWaveFinished = false;

    // Initialise.
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        // Check the state of the enum.
        if (state == SpawnState.Waiting)
        {
            // If no enemies are alive then the wave is complete.
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        // If the wave countdown is complete then start spawning.
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            waveText.text = "Next Wave In: " + waveCountdown.ToString("F2");
        }
    }

    // If the wave is completed then start a new wave or the waves are finished.
    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            finalWaveFinished = true;
            Debug.Log("You win!");
        }
        else
        {
            nextWave++;
        }
    }

    // Check if an enemy remains using a tag.
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    // Spawn waves.
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        state = SpawnState.Spawning;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.Waiting;
        yield break;
    }

    // Spawn enemies based on the spawn location.
    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning Enemy: " + enemy.name);

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }

}
