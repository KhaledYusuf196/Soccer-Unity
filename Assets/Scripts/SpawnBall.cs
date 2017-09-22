using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnBall : NetworkBehaviour {

    public GameObject ballPrefab;
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (Input.GetButtonDown("Ball Spawn"))
        {
            CmdSpawnBall();
        }
	}

    [Command]
    void CmdSpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position + 3 * transform.forward, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(ball);
    }
}
