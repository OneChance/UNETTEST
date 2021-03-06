﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerCustom : NetworkManager
{
	public void StartupHost ()
	{
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame ()
	{
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetPort ()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	void SetIPAddress ()
	{
		string ipAddress = GameObject.Find ("InputFieldIPAddress").transform.FindChild ("Text").GetComponent<Text> ().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void OnLevelWasLoaded (int level)
	{
		if (level == 0) {
			SetupMenuSceneButton ();
		} else {
			SetupOtherSceneButton ();
		}
	}

	void SetupMenuSceneButton ()
	{
		GameObject.Find ("ButtonStartupHost").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonStartupHost").GetComponent<Button> ().onClick.AddListener (StartupHost);

		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.AddListener (JoinGame);
	}

	void SetupOtherSceneButton ()
	{
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener (NetworkManager.singleton.StopHost);
	}
}
