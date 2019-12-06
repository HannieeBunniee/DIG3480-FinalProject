/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class weaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn; //where the shot come out of
    public float fireRate; //how fast the fire rate come out
    public float delay; //make it delay a bit before shotting

    public PlayerController player;

    

    private AudioSource audioSource;
    
    //======Start=======
    void Start() //called before the first frame update
    {
        
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate); //invokereapeating invoke method in time second then repeat the rate's second
    }

    //=======Function======

    void Fire()
    {
        /*if (player.hasBuff)
        {
            player.fireRate = player.fireRate * 2;
        }
        else
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }*/

        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

}
