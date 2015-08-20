using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour
{

	private PlayerHealth healthScript;
	private Image aim;

	public override void PreStartClient ()
	{
		healthScript = GetComponent<PlayerHealth> ();
		healthScript.dieEvent += DisalbePlayer;
	}

	public override void OnStartLocalPlayer ()
	{
		aim = GameObject.Find ("Aim").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void OnNetworkDestroy ()
	{
		healthScript.dieEvent -= DisalbePlayer;
	}

	void DisalbePlayer ()
	{
		GetComponent<CharacterController> ().enabled = false;
		GetComponent<PlayerShoot> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;

		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in renderers) {
			ren.enabled = false;
		}

		healthScript.dead = true;

		if (isLocalPlayer) {
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
			aim.enabled = false;
			GameObject.Find ("GameManager").GetComponent<GlobalReference> ().respawnButton.SetActive (true);
		}
	}
}
