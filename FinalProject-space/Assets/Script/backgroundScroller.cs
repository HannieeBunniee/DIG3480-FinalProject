/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    public gameController gameController; //make sure it public so u can drag gamecontroller object in

    private Vector3 startPosition;

    //=======Start===========
    void Start()
    {
        startPosition = transform.position;
        gameController = gameController.GetComponent<gameController>(); //calling for it
    }

    //========Update===========
    void Update() //called once per frame
    {
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;

        if (gameController.winCondition == true) //if the win condition on the gamecontroller script is true
        {
            scrollSpeed = -20f;
        }

        //transform.position = startPosition + Vector3.forward * newPosition;

    }
}
