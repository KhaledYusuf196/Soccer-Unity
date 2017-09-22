using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {

    public float maxAngle;
    public float minAngle;
    public float distance;
    public float height;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private void Start()
    {
        if (!isLocalPlayer)
            Destroy(this);
    }

    private void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");
        rotationX = Mathf.Clamp(rotationX, minAngle, maxAngle);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        Camera.main.transform.position = transform.position + Vector3.up*height + rotation * dir;
        Camera.main.transform.LookAt(transform.position + Vector3.up * height);
    }
}
