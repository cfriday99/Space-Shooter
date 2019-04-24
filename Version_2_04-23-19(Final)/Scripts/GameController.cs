using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioClip musicClipBG;
    public AudioClip musicClipWin;
    public AudioClip musicClipLose;
    public AudioSource musicSource;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text reqScoreText;
    public bool winCondition;

    private int score;
    private bool gameOver;
    private bool restart;


    private void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        reqScoreText.text = "Gain 200 Points to Win";
        score = 0;
        winCondition = false;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        musicSource.clip = musicClipBG;
        musicSource.Play();
        musicSource.loop = true;
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("Space_Shooter_CF");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                reqScoreText.text = "";
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 200)
        {
            gameOverText.text = "You Win!\r\nGame Created By Cameron Friday";
            gameOver = true;
            winCondition = true;
            restart = true;
            musicSource.clip = musicClipWin;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = musicClipLose;
        musicSource.Play();
        musicSource.loop = true;
    }
}
