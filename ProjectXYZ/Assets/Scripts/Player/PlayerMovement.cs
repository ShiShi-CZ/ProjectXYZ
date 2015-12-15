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
	void FixedUpdate () {
        MoveAndTurn();
	}

    private void Animate()
    {
        //throw new NotImplementedException();
    }

    private void MoveAndTurn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Vector3 lookAt = Vector3.zero;

        if (Physics.Raycast(ray, out hitInfo))
        {
            lookAt = hitInfo.point - transform.position;
            lookAt.y = 0;
            transform.LookAt(transform.position + lookAt, Vector3.up);
            
            if(Input.GetMouseButtonDown(0))
            {
                rb.AddForce(lookAt.normalized*1000000);
            }

        }

    }

}
