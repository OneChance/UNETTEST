using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

	private int damage = -25;
	private float range = 200;
	[SerializeField]
	private Transform
		camTransform;
	private RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			CheckIfShooting ();
		}
	}

	void CheckIfShooting ()
	{
		if (!isLocalPlayer) {
			return;
		} else {
			Shoot ();
		}
	}
	
	void Shoot ()
	{
		if (Physics.Raycast (camTransform.TransformPoint (0, 0, 0.5f), camTransform.forward, out hit, range)) {
			if (hit.transform.tag == "Player") {
				string identity = hit.transform.name;
				CmdTellServerWhoWasShot (identity, damage);
			} else if (hit.transform.tag == "Zombie") {
				string identity = hit.transform.name;
				CmdTellServerZombieWasShot (identity, damage);
			}
		}
	}

	[Command]
	void CmdTellServerWhoWasShot (string identity, int damage)
	{
		GameObject go = GameObject.Find (identity);
		go.GetComponent<PlayerHealth> ().ChangeHealth (damage);
	}

	[Command]
	void CmdTellServerZombieWasShot (string identity, int damage)
	{
		GameObject go = GameObject.Find (identity);
		go.GetComponent<ZombieHealth> ().DeductHealth (damage);
	}
}
