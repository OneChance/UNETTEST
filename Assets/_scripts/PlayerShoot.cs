using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

	private float damage = 25;
	private float range = 200;
	[SerializeField]
	private Transform camTransform;
	private RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
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
				string identity = transform.name;
				cmdTellServerWhoWasShot (identity, damage);
			}
		}
	}

	[Command]
	void cmdTellServerWhoWasShot (string identity, float damage)
	{
		GameObject go = GameObject.Find(identity);
	}
}
