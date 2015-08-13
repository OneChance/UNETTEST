using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

    [SerializeField] 
    Transform myTransform;

    [SerializeField] 
    float lerpRate = 15;
	
	// Update is called once per frame
	void FixedUpdate () {
        TransmitPosition();
        LerpPos();
	}

    void LerpPos() {
        if (!isLocalPlayer) {
            myTransform.position = Vector3.Lerp(myTransform.position,syncPos,Time.deltaTime*lerpRate);
        }
    }

    [Command]
    void CmdProviderPositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer) {
            CmdProviderPositionToServer(myTransform.position);
        }
    }
}