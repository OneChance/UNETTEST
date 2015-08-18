using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerRespawn : NetworkBehaviour
{

	private PlayerHealth healthScript;
	private Image aim;
	private GameObject respawnButton;

	// Use this for initialization
	void Start ()
	{
		aim = GameObject.Find ("Aim").GetComponent<Image> ();
		healthScript = GetComponent<PlayerHealth> ();
		healthScript.respawnEvent += EnablePlayer;
		SetRespawnButton ();
	}

	void SetRespawnButton ()
	{
		if (isLocalPlayer) {
			respawnButton = GameObject.Find ("GameManager").GetComponent<GlobalReference> ().respawnButton;
			respawnButton.GetComponent<Button> ().onClick.AddListener (CommenceRespawn);
			respawnButton.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void EnablePlayer ()
	{

	}

	void CommenceRespawn ()
	{
		CmdRespawnOnServer();
	}

	[Command]
	void CmdRespawnOnServer(){
		healthScript.ResetHealth();
	}
}
