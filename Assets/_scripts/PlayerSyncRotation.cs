using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour
{

    [SyncVar]
    private Quaternion sycnPlayerRotation;

    [SyncVar]
    private Quaternion sycnCamRotation;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform camTransform;

    [SerializeField]
    private float lerpRate = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TransmitRotation();
        LerpRotation();
	}

    void LerpRotation() {
        if (!isLocalPlayer) {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, sycnPlayerRotation, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, sycnCamRotation, Time.deltaTime * lerpRate);
        }  
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion playerRotation,Quaternion camRotation) {
        sycnPlayerRotation = playerRotation;
        sycnCamRotation = camRotation;
    }

    [ClientCallback]
    void TransmitRotation() {
        if (isLocalPlayer) {
            CmdProvideRotationToServer(playerTransform.rotation,camTransform.rotation);
        }
    }
}
