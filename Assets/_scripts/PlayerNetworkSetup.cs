using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera playerCam;

    [SerializeField]
    AudioListener playerLis;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer) {
            GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
            playerCam.enabled = true;
            playerLis.enabled = true;
        }
	}
}