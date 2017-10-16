using UnityEngine;
using System.Collections;

public class ClothCollision : MonoBehaviour {
    private Rigidbody body;
    Transform offset;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        offset = transform;
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void LateUpdate()
    {
        transform.position = offset.position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        body.velocity = collision.rigidbody.velocity;
    }
}
