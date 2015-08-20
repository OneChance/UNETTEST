using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerLatency : NetworkBehaviour
{

	private NetworkClient nClient;
	private Text latency;

	public override void OnStartLocalPlayer ()
	{
		nClient = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ().client;
		latency = GameObject.Find ("Latency").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ShowLatency ();
	}

	void ShowLatency ()
	{
		if (isLocalPlayer) {
			latency.text = nClient.GetRTT ().ToString ();
		}
	}
}
