/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    //=====Start====
    void Start() //called before the first frame update
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    //=====Function?=======
    IEnumerator Evade() //code to make ship slide left/right like dodging
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x); //see if enemy ship is on the postive +1 side of the area, mathf will multiply dodge by -1
            yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y) );
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range (maneuverWait.x, maneuverWait.y) );
        }
    }

    //======Updates=======
    void FixedUpdate() 
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed); // <=== (x,y,z)

        rb.position = new Vector3 //making sure the ship cant fly out of the camera map
       (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), //x
            0.0f, //y
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax) //z
       );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        
    }

}
