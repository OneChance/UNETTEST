using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerId : NetworkBehaviour
{

	[SyncVar]
	private string
		identity;
	private NetworkInstanceId pNetId;
	private Transform myTransform;

	public override void OnStartClient ()
	{
		GetNetIdentity ();
		SetIdentity ();
	}

	// Use this for initialization
	void Awake ()
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (myTransform.name == "" || myTransform.name == "Player(Clone)") {
			//SetIdentity();
		}
	}

	void SetIdentity ()
	{
		if (isLocalPlayer) {
			myTransform.name = identity;
		} else {
			myTransform.name = identity;
		}
	}

	[Client]
	void GetNetIdentity ()
	{
		pNetId = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyIdentity (MakeIdentity ());
	}

	string MakeIdentity ()
	{
		return "Player" + pNetId.ToString ();	
	}

	[Command]
	void CmdTellServerMyIdentity (string identity)
	{
		this.identity = identity;
	}
}

