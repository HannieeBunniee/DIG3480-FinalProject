using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private gameController gameController;
    public float timeLeft = 60.0f;
    public Text countdownText;

    void Start()
    {
        //gameController = gameController.GetComponent<gameController>(); //calling for it
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            gameController.GameOver();
        }
    }
}