using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieAttack : NetworkBehaviour
{

	private float attackRate = 3;
	private float nextAttack = 0;
	private int damage = -10;
	private float minDistance = 1.3f;
	private float currentDistance;
	private Transform myTransform;
	private ZombieTarget targetScript;
	[SerializeField]
	private Material
		zombieGreen;
	[SerializeField]
	private Material
		zombieRed;
	private Renderer zombieRender;

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		zombieRender = GetComponent<Renderer> ();
		targetScript = GetComponent<ZombieTarget> ();
		if (isServer) {
			StartCoroutine (Attack ());
		}
	}

	void CheckIfTargetInRange ()
	{
		if (targetScript.targetTransform != null) {

			currentDistance = Vector3.Distance (targetScript.targetTransform.position, myTransform.position);

			if (currentDistance < minDistance && Time.time > nextAttack) {
				nextAttack = Time.time + attackRate;
				targetScript.targetTransform.GetComponent<PlayerHealth> ().ChangeHealth (damage);
				StartCoroutine (ChangeZombieMat ()); 
				RpcChangeZombieAppearance ();
			}
		} 
	}

	[ClientRpc]
	void RpcChangeZombieAppearance ()
	{
		StartCoroutine (ChangeZombieMat ()); 
	}

	IEnumerator ChangeZombieMat ()
	{
		zombieRender.material = zombieRed;
		yield return new WaitForSeconds (attackRate * 0.5f);
		zombieRender.material = zombieGreen;
	}

	IEnumerator Attack ()
	{
		for (;;) {
			yield return new WaitForSeconds (0.2f);
			CheckIfTargetInRange ();
		}
	}
}
