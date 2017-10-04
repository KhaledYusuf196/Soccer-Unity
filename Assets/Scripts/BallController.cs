using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    private Rigidbody ball;
    public float magnusFactor = 100f;
	// Use this for initialization
	void Start () {
        ball = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ball.AddForce(magnusFactor * Vector3.Cross(ball.angularVelocity, ball.velocity));
	}
}
