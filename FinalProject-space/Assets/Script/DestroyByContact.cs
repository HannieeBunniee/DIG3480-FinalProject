/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private gameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<gameController>();
        }
        /*if (gameController == null) //this giving me dumb error man
        {
            Debug.Log("Cannot find 'gameController' script");
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name); //checking to see why asteroid disapear when test play game

        if (other.tag == ("Boundary") || other.tag == ("Enemy") || other.tag == ("pickup")) //make sure that if it the boundary it wont collide and destroy each other
        {
            return;
        }

        //Explosion
        if (explosion != null) //check to see if they have explosion or not to use the code (added if statement on adding enemy bolt part)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        /*if (other.tag == ("Player") && other.tag == ("pickup"))
        {
            return;
        }*/

        if (other.tag == ("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();//calling the code from gamecontroller to show restart/winText
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
