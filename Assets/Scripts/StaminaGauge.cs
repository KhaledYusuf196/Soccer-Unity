using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class StaminaGauge : NetworkBehaviour {
    private Text text;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            Destroy(this);
        text = FindObjectOfType<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        text.GetComponent<Text>().text = "Stamina : " + GetComponent<PlayerController>().getStam() + "\nPower : " + GetComponent<BallController>().getPower();
    }
}
