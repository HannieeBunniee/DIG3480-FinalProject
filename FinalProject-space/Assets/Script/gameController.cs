/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // so u can recall/restart the scense

public class gameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text winText;
    private int score;

    public AudioSource musicSource;
    public AudioClip musicClipWin;
    public AudioClip musicClipLose;
    public AudioClip musicBackground;

    public bool winCondition;
    private bool gameOver;
    private bool restart;

    public ParticleSystem starfield;
    public ParticleSystem starDistant;

    //======Start===========
    private void Start()
    {
        musicSource.clip = musicBackground;
        musicSource.Play();
        winCondition = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (spawnWave()); //<-- have to use starcoroutine (function()); to call a function.. this is getting more confusing
    }

    //===========Updates============
    private void Update()
    {
        if (restart)
        {
            //if (Input.anyKey) i misread it into ANY key that was pressed down
            if (Input.GetKeyDown (KeyCode.F))
            {
                SceneManager.LoadScene("FinalProject"); //the scense name, restarting the scense after press R
            }
        }
    }
    private void LateUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    //=====Functions=====
    IEnumerator spawnWave() // <--- cant be void when using waitforsecond v
    {
        yield return new WaitForSeconds(startWait);

        while (true) //so player dont run out of asterod to shot/dodge
        {
            for (int i = 0; i < hazardCount; i++) //make it loops to spawn asteroid 
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)]; //making it pick one random asteroid model from the list to spawn
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //having x as random.range so it doesnt spawn in straight line
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


            if (gameOver) //check to see if gameover is true to get out of loop
            {
                restartText.text = "Press the 'F' Key to Restart";
                restart = true;
                break; // break out of while loop
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
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //cant lose after winning, also stoping win audio repeating
            foreach (GameObject enemy in enemies)
                GameObject.Destroy(enemy);

            winText.text = "You win!\nCreated by Hanniee";
            /*musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = musicClipWin;
            musicSource.Play();
            winCondition = true;*/
            gameOver = true;
            restart = true;
            afterWin();
        }
    }
    public void GameOver()
    {
        winText.text = "Game Over :P!";
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.clip = musicClipLose;
        musicSource.Play();
        gameOver = true;
    }

    void afterWin() //just for the dumb stars :T
    {
        winCondition = true;
        var main1 = starfield.main;
        main1.startSpeedMultiplier = 5.0f;
        var main2 = starDistant.main;
        main2.startLifetime = 50f;
        main2.startSpeed = 30.0f;
        musicSource.loop = true;
        musicSource.clip = musicClipWin;
        musicSource.Play();
    }

}
