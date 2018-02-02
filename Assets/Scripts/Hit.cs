using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
    public Vector3 stpos;
    // Use this for initialization

    public void hit()
    {
        transform.position = stpos;
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-30,30), Random.Range(0, 30.0f), Random.Range(30, 80.0f));

    }

    
}
