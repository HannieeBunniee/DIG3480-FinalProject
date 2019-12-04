/*************************************************************
 * ==*==*==*==*==*==*   Hanniee Tran   ==*==*==*==*==*==*==*==
 * ===================    DIG 3480    ========================
 * ============    Computer As A Medium    ===================
 * ==*==*==*==*==*==   Final Project   ==*==*==*==*==*==*==*==
 ************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
