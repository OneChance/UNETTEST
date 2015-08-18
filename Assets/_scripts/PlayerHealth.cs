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
    private bool shoudDie = false;
    public bool dead = false;

    public delegate void DieDelegate();
    public event DieDelegate dieEvent;

    public delegate void RespawnDelegate();
    public event RespawnDelegate respawnEvent;

	// Use this for initialization
	void Start ()
	{
		healthText = GameObject.Find ("Health").GetComponent<Text> ();
		SetHealthText ();
	}
	
	// Update is called once per frame
	void Update ()
	{
        CheckCondition();
	}

    void CheckCondition() {
        if (health <= 0 && !shoudDie && !dead) {
            shoudDie = true;
        }

        if (health <= 0 && shoudDie) {
            if (dieEvent != null) {
                dieEvent();
            }
            shoudDie = false;
        }

        if (health > 0 && dead) {
            if (respawnEvent != null) {
                respawnEvent();
                dead = false;
            }
        }
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

    public void ResetHealth() {
        health = 100;
    }
}
