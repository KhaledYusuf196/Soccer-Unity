using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
    public Vector3 stpos;
    // Use this for initialization

    public void hit()
    {
        transform.position = stpos;
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-6,6), Random.Range(0, 5.0f), Random.Range(30, 40.0f));

    }

    
}
