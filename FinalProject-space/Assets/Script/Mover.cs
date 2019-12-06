/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    //public float boltSpeed;

    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; //shooting the bolt straight forward

        /*if (gameObject.CompareTag("Player"))
        {
            rb.velocity = transform.forward * boltSpeed; //shooting the bolt straight forward
        }
        else 
        {
            rb.velocity = transform.forward * speed; //shooting the bolt straight forward
        }*/


    }
}
