/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary //so it more organize
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    public bool hasBuff;
    public float buffTimer;

    private Rigidbody rb;

    

    public AudioSource musicSource;
    public AudioClip shottingAudio;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        buffTimer = 5f; //buff timer 5 second
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //make the bolt fire/spawn at certain rate
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            musicSource.clip = shottingAudio;
            musicSource.Play();

        }

        CheckIfBuffActive(); //function

     
    }

    private void CheckIfBuffActive() // check if buff is active
    {
        if (hasBuff)
        {
            buffTimer -= Time.deltaTime; //countdown timer by 1 second
            if (buffTimer <= 0)
            {
                hasBuff = false;
                ResetTimer();
            }
        }
        else if (!hasBuff) // go down to 0 and reset to firerate
        {
            fireRate = .5f;
        }
    }

    private void ResetTimer()
    {
        buffTimer = 5f; //5 second
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //player control move left/right
        float moveVertical = Input.GetAxis("Vertical");//control move up/down

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // 0.0f on Y cordniate cuz dont need it
        rb.velocity = movement * speed;

        rb.position = new Vector3 //making sure the ship cant fly out of the camera map
        (
             Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), //x
             0.0f, //y
             Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax) //z
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt); //tilt code
    }
    private void OnTriggerEnter(Collider other) // see if the player collide with the pickup
    {
        if (other.tag == ("pickup"))
        {
            Destroy(other.gameObject);
            hasBuff = true;
           
            fireRate  = fireRate / 3; // cutting firerate by half (increase speed)
            
        }
    }
}
