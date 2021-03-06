﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    private Animator playerAnimator;
    private Rigidbody body;
    private int stamina;
    private bool sprint;
    private const float Max_Speed = 20.0f;
    private const float Xlration = 2.0f;
    private const int Max_Stam = 100;
    private const int sprintSpeedFactor = 2;
    private const int sprintAccFactor = 2;
	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        if (!isLocalPlayer)
            Destroy(this);
        body = GetComponent<Rigidbody>();
        stamina = Max_Stam;
        sprint = false;
        
	}
	
	// Update is called once per frame
	void Update() {
        float sideSpeed = Vector3.Dot(-transform.right, body.velocity / Max_Speed);
        float forwardSpeed = Vector3.Dot(transform.forward, body.velocity / Max_Speed);
        if (sideSpeed < 0)
        {
            playerAnimator.SetFloat("side", -1);
            sideSpeed *= -1;
        }
        else
        {
            playerAnimator.SetFloat("side", 1);
        }
        if (forwardSpeed < 0)
        {
            playerAnimator.SetFloat("forward", -1);
            forwardSpeed *= -1;
        }
        else
        {
            playerAnimator.SetFloat("forward", 1);
        }
        playerAnimator.SetLayerWeight(1, forwardSpeed);
        playerAnimator.SetLayerWeight(2, sideSpeed);

        transform.forward = Vector3.Cross(Camera.main.transform.right, transform.up).normalized;
        Vector3 direction = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;
        if(Input.GetButtonDown("Sprint"))
            sprint = !sprint;

        if (sprint && stamina > 0)
        {
            stamina = Mathf.Clamp(stamina - 1, 0, Max_Stam);
            move(direction, Max_Speed * sprintSpeedFactor, Xlration * sprintAccFactor);
        }
        else
        {
            sprint = false;
            stamina = Mathf.Clamp(stamina + 1, 0, Max_Stam);
            move(direction, Max_Speed, Xlration);
        }
        
	}

    private void move(Vector3 direction, float goalVelocity, float deltaVelocity)
    {
        direction = direction.normalized;
        body.velocity += direction * deltaVelocity;
        if (body.velocity.magnitude > goalVelocity)
        {
            body.velocity -= body.velocity.normalized * Mathf.Min(deltaVelocity, body.velocity.magnitude - goalVelocity);
        }
    }

    public int getStam()
    {
        return stamina;
    }
    
}
