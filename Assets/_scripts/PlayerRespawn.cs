﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerRespawn : NetworkBehaviour
{

	private PlayerHealth healthScript;
	private Image aim;
	private GameObject respawnButton;

	public override void PreStartClient(){
		healthScript = GetComponent<PlayerHealth> ();
		healthScript.respawnEvent += EnablePlayer;
	}
	
	public override void OnStartLocalPlayer(){
		aim = GameObject.Find ("Aim").GetComponent<Image> ();		
		SetRespawnButton ();
	}

	public override void OnNetworkDestroy() {
		healthScript.respawnEvent -= EnablePlayer;
	}

	void SetRespawnButton ()
	{
		if (isLocalPlayer) {
			respawnButton = GameObject.Find ("GameManager").GetComponent<GlobalReference> ().respawnButton;
			respawnButton.GetComponent<Button> ().onClick.AddListener (CommenceRespawn);
			respawnButton.SetActive(false);
		}
	}

	void EnablePlayer ()
	{
		GetComponent<CharacterController>().enabled = true;
		GetComponent<PlayerShoot>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;
		
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer ren in renderers) {
			ren.enabled = true;
		}

		if (isLocalPlayer) {
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			aim.enabled = true;
			respawnButton.SetActive(false);
		}
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
