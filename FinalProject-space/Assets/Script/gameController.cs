/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // to use UI text
using System.Threading; //for the countdown timer i guess?
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
    public Text countdownText;
    //public Text hardmodeText;
    public int timeLeft = 60;
    private int score;
    private int hazardStart;
    

    public AudioSource musicSource;
    public AudioClip musicClipWin;
    public AudioClip musicClipLose;
    public AudioClip musicBackground;

    //public Mover moverscript;

    public bool winCondition;
    private bool gameOver;
    private bool restart;

    public ParticleSystem starfield;
    public ParticleSystem starDistant;

    //======Start===========
    private void Start()
    {
        // moverscript = moverscript.GetComponent<Mover>(); //calling for it
        Time.timeScale = 1; // just making sure the timescale is right  
        musicSource.clip = musicBackground;
        musicSource.Play();
        winCondition = false;
        gameOver = false;
        restart = false;
        //hardmodeText.text = "Press 'H' to enter Hardmode";
        countdownText.text = "";
        restartText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
       // StartCoroutine (spawnWave()); //<-- have to use starcoroutine (function()); to call a function.. this is getting more confusing
    }

    //===========Updates============
    private void Update()
    {
        //countdownText.text = ("Time left: " + timeLeft);
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

        while (gameOver == false) //so player dont run out of asterod to shot/dodge
        {
            for (int i = 0; i < hazardCount; i++) //make it loops to spawn asteroid 
            {
                GameObject hazard = hazards[Random.Range(hazardStart, hazards.Length)]; //making it pick one random asteroid model from the list to spawn
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

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
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
        //main1.startSpeedMultiplier = 10.0f;
        main1.simulationSpeed = 40f;
        var main2 = starDistant.main;
        //main2.startLifetime = 50f;
        //main2.startSpeed = 60.0f;
        main2.simulationSpeed = 30f;
        musicSource.loop = true;
        musicSource.clip = musicClipWin;
        musicSource.Play();
    }

    //Main menu buttons functions
    public void endlessMode()
    {
        hazardStart = 0;
        StartCoroutine(spawnWave()); //<-- have to use starcoroutine (function()); to call a function.. this is getting more confusing
        
    }

    public void normalMode()
    {
        hazardStart = 0;
        StartCoroutine(spawnWave()); //<-- have to use starcoroutine (function()); to call a function.
    }
    
    public void hardMode()
    {
        countdownText.text = ("Time left: " + timeLeft);
        hazardStart = 3;
        hazardCount = 20;
        //moverscript.speed = -10f;
        StartCoroutine("LoseTime");
        StartCoroutine(spawnWave()); //<-- have to use starcoroutine (function()); to call a function.

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
