/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
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

    private Rigidbody rb;
    

    public AudioSource musicSource;
    public AudioClip shottingAudio;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
