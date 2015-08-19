using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour {

    private PlayerHealth healthScript;
    private Image aim;

	// Use this for initialization
	void Start () {
        aim = GameObject.Find("Aim").GetComponent<Image>();
        healthScript = GetComponent<PlayerHealth>();

        healthScript.dieEvent += DisalbePlayer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDisable() {
        healthScript.dieEvent -= DisalbePlayer;
    }

    void DisalbePlayer() {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer ren in renderers) {
            ren.enabled = false;
        }

        healthScript.dead = true;

        if (isLocalPlayer) {
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            aim.enabled = false;
			GameObject.Find ("GameManager").GetComponent<GlobalReference> ().respawnButton.SetActive(true);
        }
    }
}
