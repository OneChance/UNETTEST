using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour
{

    [SyncVar]
    private Quaternion syncPlayerRotation;

    [SyncVar]
    private Quaternion syncCamRotation;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform camTransform;

    private Quaternion lastPlayerRot;
    private Quaternion lastCamRot;

    private float threshold = 5f;

    [SerializeField]
    private float lerpRate = 10;

	// Use this for initialization
	void Start () {
	
	}

    void Update() {
        LerpRotation();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitRotation();
	}

    void LerpRotation() {
        if (!isLocalPlayer) {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        }  
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion playerRotation,Quaternion camRotation) {
        syncPlayerRotation = playerRotation;
        syncCamRotation = camRotation;
    }

    [Client]
    void TransmitRotation() {
        if (isLocalPlayer) {
            if (Quaternion.Angle(playerTransform.rotation, lastPlayerRot) > threshold || Quaternion.Angle(camTransform.rotation, lastCamRot) > threshold) {
                CmdProvideRotationToServer(playerTransform.rotation, camTransform.rotation);
                lastCamRot = camTransform.rotation;
                lastPlayerRot = playerTransform.rotation;
            }       
        }
    }
}
