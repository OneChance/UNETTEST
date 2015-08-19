using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieTarget : NetworkBehaviour
{

	private NavMeshAgent agent;
	private Transform myTransform;
	public Transform targetTransform;
	private LayerMask raycastLayer;
	private float radius = 100;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		myTransform = transform;
		raycastLayer = 1 << LayerMask.NameToLayer ("Player");

		if(isServer){
			StartCoroutine(DoCheck());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	}

	void SearchForTarget ()
	{
		if (isServer) {

			if(targetTransform == null){
				Collider[] hitColliders = Physics.OverlapSphere (myTransform.position, radius, raycastLayer);
				if (hitColliders.Length > 0) {
					targetTransform = hitColliders [Random.Range (0, hitColliders.Length)].transform;
				}
			}else{
				if(!targetTransform.GetComponent<BoxCollider>().enabled){
					targetTransform = null;
				}
			}
		}
	}

	void MoveToTarget ()
	{
		if(targetTransform!=null && isServer){
			agent.SetDestination(targetTransform.position);
		}
	}

	
	IEnumerator DoCheck(){
		for(;;){
			SearchForTarget ();
			MoveToTarget ();
			yield return new WaitForSeconds(0.2f);
		}
	}
}
