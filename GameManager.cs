using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public static float xBound = 14;
    public static float yBound = 9;
    public static int score = 0;
    public static int highScore = 0;
    public static float volume = 0.5f;
    public bool gameOver = false;
    public TextMeshProUGUI scoreText, scoreRecordText;
    public GameObject poseidon, hades, HarpyPrefab, MinotaurPrefab, player, gameOverMenu;

    int waveCount = 0;
    float xStartPoint = xBound + 2;
    float yStartPoint = -yBound + 3;
    bool hadesTurn = false;
    bool stageClear = false;
    bool clearingStage = false;
    PlayerController playerController;
    AudioSource gameAudio;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        gameAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
        gameAudio.volume = volume;
        if (gameOver && !stageClear && !clearingStage)
        {
            StartCoroutine(ClearStage());
            clearingStage = true;
        }
        else if(!gameOver)
        {
            StageCheck();
            DeathCheck();
        }
    }
    void StageCheck()
    {
        if (!GameObject.FindWithTag("Enemy"))
        {
            waveCount++;
            SpawnWave();
        }
    }
    void DeathCheck()
    {
        if (playerController.isDead)
        {
            gameOverMenu.SetActive(true);
            if (score > highScore)
                highScore = score;
            scoreRecordText.text = "Score: " + score + "\n" + "High Score: " + highScore;
            gameOver = true;
        }
    }
    void SpawnWave()
    {
        print("Wave: " + waveCount);
        //Spawns a boss every 3 waves.
        if (waveCount % 3 == 0)
        {
            StartCoroutine(SpawnBoss());
            print("Boss wave!");
        }
        else
        {
            StartCoroutine(SpawnHarpyWave());
            StartCoroutine(SpawnMinotaurWave());
        }
    }
    IEnumerator ClearStage()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        while(enemy)
        {
            Destroy(enemy);
            enemy = GameObject.FindWithTag("Enemy");
            yield return null;
        }
        stageClear = true;
    }
    IEnumerator SpawnHarpyWave()
    {
        float xStep = 0;
        float yStep = 0;
        float lerpEnd = xBound - 3;
        const int maxHarpyCount = 9;
        int harpiesToSpawn = waveCount;
        Vector3 spawnPos = new Vector3(xStartPoint, yStartPoint);
        GameObject harpyObj;
        Harpy harpyScript;

        for (int counter = 0; counter < harpiesToSpawn && counter < maxHarpyCount; counter++)
        {
            spawnPos = new Vector3(xStartPoint, yStartPoint + yStep);
            harpyObj = Instantiate(HarpyPrefab, spawnPos, HarpyPrefab.transform.rotation);
            harpyScript = harpyObj.GetComponent<Harpy>();
            StartCoroutine(harpyScript.SetMovement(xStartPoint, lerpEnd));
            yStep += 6;
            if (counter >= 2 && (counter + 1) % 3 == 0)
            {
                xStep -= 3;
                lerpEnd += xStep;
                yStep = 0;
                yield return new WaitForSeconds(5);
            }
            yield return null;
        }
    }
    IEnumerator SpawnMinotaurWave()
    {
        Vector3 spawnPos = new Vector3(-GameManager.xBound - 5, 6);
        GameObject minotaurObj = Instantiate(MinotaurPrefab, spawnPos, MinotaurPrefab.transform.rotation);
        Minotaur minotaurScript = minotaurObj.GetComponent<Minotaur>();
        StartCoroutine(minotaurScript.SetPosition(spawnPos.x, -GameManager.xBound + 1));
        yield return null;
    }
    IEnumerator SpawnBoss()
    {
        //Move the boss into the stage, and then if the boss is still alive, spawn
        //a harpy wave and a minotaur wave.
        Vector3 spawnPos = new Vector3(GameManager.xBound + 5, 0);
        GameObject bossObj;
        if(hadesTurn)
        {
            bossObj = Instantiate(hades, spawnPos, hades.transform.rotation);
            hadesTurn = false;
        }
        else
        {
            bossObj = Instantiate(poseidon, spawnPos, poseidon.transform.rotation);
            hadesTurn = true;
        }
        Boss bossScript = bossObj.GetComponent<Boss>();
        StartCoroutine(bossScript.SetPosition(spawnPos.x, spawnPos.x - 11));
        yield return new WaitForSeconds(15);
        if(bossObj != null)
        {
            StartCoroutine(SpawnHarpyWave());
            StartCoroutine(SpawnMinotaurWave());
        }
        yield return null;
    }
}
