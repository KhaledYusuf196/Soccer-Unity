using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour {

    private const int Max_Power = 100;
    private const int Min_Power = 20;
    private bool isShooting;
    private bool chargedShot;
    private bool isCharging;
    private int chargeIncrement;
    private float power;

    private void Start()
    {
        isShooting = false;
        isCharging = false;
        chargedShot = false;
        chargeIncrement = 1;
        power = Min_Power;
         
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isCharging = true;
            isShooting = false;
            power = Min_Power;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isCharging = false;
            isShooting = true;
            chargedShot = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!isCharging)
                isShooting = true;
            chargedShot = false;
            power = Min_Power;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isShooting = false;
        }
        if (isCharging)
        {
            if (power <= Min_Power)
                chargeIncrement = 1;
            else if (power >= Max_Power)
                chargeIncrement = -1;
            power = power + chargeIncrement;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isLocalPlayer)
            return;
        GameObject ball = collision.gameObject;
        if (ball.tag == "Ball" && isShooting)
        {
            CmdShot(ball, power, Camera.main.transform.forward);
            if (chargedShot)
            {
                isShooting = false;
            }
        }
    }

    public float getPower()
    {
        return power;
    }

    [Command]
    private void CmdShot(GameObject ball, float pow, Vector3 dir)
    {
        ball.GetComponent<Rigidbody>().velocity = pow * (dir + 0.5f * Vector3.up).normalized;
    }
}
