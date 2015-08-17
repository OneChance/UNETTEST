using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{

	[SyncVar(hook="OnHealthChange")]
	private int
		health = 100;
	private Text healthText;

	// Use this for initialization
	void Start ()
	{
		healthText = GameObject.Find ("Health").GetComponent<Text> ();
		SetHealthText ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void SetHealthText ()
	{	
		if(isLocalPlayer){
			healthText.text = "Health " + health;
		}
	}

	public void ChangeHealth (int v)
	{
		health += v;
	}

	void OnHealthChange (int v)
	{
		health = v;
		SetHealthText ();
	}

}
