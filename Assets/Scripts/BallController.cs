using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BallController : NetworkBehaviour {
    private Rigidbody ball;
    public float magnusFactor = 100f;
	// Use this for initialization
	void Start () {
        if (!isServer)
            Destroy(this);
        ball = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ball.AddForce(magnusFactor * Vector3.Cross(ball.angularVelocity, ball.velocity));
	}
}
