using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieId : NetworkBehaviour
{

	[SyncVar]
	public string
		zombieId;
	private Transform myTransform;

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
	}

	void SetZombieId ()
	{
		myTransform.name = zombieId;
	}

	// Update is called once per frame
	void Update ()
	{
		if (myTransform.name == "" || myTransform.name == "Zombie(Clone)") {
			myTransform.name = zombieId;
		}
	}
}
