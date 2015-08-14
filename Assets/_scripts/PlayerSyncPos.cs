using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

[NetworkSettings(channel=0,sendInterval=0.1f)]
public class PlayerSyncPos : NetworkBehaviour
{
    [SyncVar (hook="WhenSync")]
    private Vector3 syncPos;

    [SerializeField] 
    Transform myTransform;

    [SerializeField] 
    float lerpRate;
    float normalLerpRate = 18;
    float fastLerpRate = 27;

    private Vector3 lastPos;
    private float threshold = 0.5f;

    private NetworkClient nClient;
    private Text latency;

    private List<Vector3> posList;
    [SerializeField]private bool useList = false;
    private float closeEnough  = 0.1f;

    void Start() {
        nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
        latency = GameObject.Find("Latency").GetComponent<Text>();
        lerpRate = normalLerpRate;
        posList = new List<Vector3>();
    }

    void Update()
    {
        LerpPos();
        ShowLatency();
    }

	void FixedUpdate () {
        TransmitPosition();
	}

    void LerpPos() {
        if (!isLocalPlayer) {
            if (useList){
                ListLerp();
            } else {
                NormalLerp();
            }  
        }
    }

    void NormalLerp() {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }

    void ListLerp() {
        if (posList.Count > 0) {
            myTransform.position = Vector3.Lerp(myTransform.position, posList[0], Time.deltaTime * lerpRate);

            if (Vector3.Distance(myTransform.position, posList[0]) < closeEnough) {
                posList.RemoveAt(0);
            }

            if (posList.Count > 10)
            {
                lerpRate = fastLerpRate;
            }
            else {
                lerpRate = normalLerpRate;
            }

            Debug.Log(posList.Count);
        }
    }

    [Command]
    void CmdProviderPositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer && Vector3.Distance(myTransform.position,lastPos) > threshold) {
            CmdProviderPositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [ClientCallback]
    void WhenSync(Vector3 pos) {
         syncPos = pos;
         posList.Add(pos);  
    }

    void ShowLatency() {
        if (isLocalPlayer) {
            latency.text = nClient.GetRTT().ToString();
        }
    }
}