using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Controls the Movement of the player. This class is unfinished and I have not the best Unity API Skills atm. 
/// Someone else should do it.
/// </summary>
public class PlayerMovement : MonoBehaviour {


    Rigidbody rb;
	void Awake () { 
        rb = GetComponent<Rigidbody>();
	}
	
	// Unoptimized
	void Update () {
        if (Input.GetMouseButtonDown(3))
        {
            Move();
        }

        Turn();
	}

    private void Animate()
    {
        //throw new NotImplementedException();
    }

    private void Move()
    {
        
    }

    private void Turn()
    {

    }

}
