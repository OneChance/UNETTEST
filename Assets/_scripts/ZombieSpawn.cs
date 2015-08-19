using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieSpawn : NetworkBehaviour
{

	[SerializeField]
	GameObject zombiePrefab;
	[SerializeField]
	GameObject zombieSpawn;
	private int counter;
	private int numberOfZombies = 30;

	//这个方法只会在服务器端运行
	public override void OnStartServer ()
	{
		for (int i=0; i<numberOfZombies; i++) {
			SpawnZombies ();
		}
	}

	void SpawnZombies ()
	{
		GameObject go = GameObject.Instantiate (zombiePrefab, zombieSpawn.transform.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (go);
		counter++;
		go.GetComponent<ZombieId> ().zombieId = "Zombie" + counter;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

}
