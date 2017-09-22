using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerCamera : NetworkManager {
    public Transform sceneCamera;
    public GameObject ball;
    public float cameraRotationRadius = 24f;
    public float cameraRotationSpeed = 3f;
    public bool canRotate = true;

    private float rotation;

    public override void OnStartClient(NetworkClient client)
    {
        canRotate = false;
        FindObjectOfType<AudioSource>().enabled = true;
    }
    public override void OnStartHost()
    {
        canRotate = false;
        FindObjectOfType<AudioSource>().enabled = true;
    }
    public override void OnStopClient()
    {
        canRotate = true;
        FindObjectOfType<AudioSource>().enabled = false;
    }
    public override void OnStopHost()
    {
        canRotate = true;
        FindObjectOfType<AudioSource>().enabled = false;
    }

    private void Update()
    {
        if (!canRotate) return;
        rotation += cameraRotationSpeed * Time.deltaTime;
        sceneCamera.position = Vector3.zero;
        sceneCamera.rotation = Quaternion.Euler(0f, rotation, 0f);
        sceneCamera.Translate(0f, cameraRotationRadius, -cameraRotationRadius);
        sceneCamera.LookAt(Vector3.zero);
    }

}
