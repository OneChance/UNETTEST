using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieSpawn : NetworkBehaviour
{

	[SerializeField]
	GameObject zombiePrefab;
	private GameObject[] zombieSpawns;
	private int counter;
	private int numberOfZombies = 10;
	private int maxNumberOfZombies = 30;
	private float waveRate = 10;
	private bool isSpawnActived = true;

	//这个方法只会在服务器端运行
	public override void OnStartServer ()
	{
		zombieSpawns = GameObject.FindGameObjectsWithTag("ZombieSpawn");
		StartCoroutine (ZombieSpawner ());
	}

	IEnumerator ZombieSpawner(){
		for (;;) {
			yield return new WaitForSeconds(waveRate);
			GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
			if(zombies.Length<maxNumberOfZombies){
				CommenceSpawn();
			}
		}
	}

	void CommenceSpawn(){
		if (isSpawnActived) {
			for(int i=0;i<numberOfZombies;i++){
				SpawnZombies(zombieSpawns[Random.Range(0,zombieSpawns.Length)].transform.position);
			}
		}
	}

	void SpawnZombies (Vector3 spawnPos)
	{
		GameObject go = GameObject.Instantiate (zombiePrefab, spawnPos, Quaternion.identity) as GameObject;
		counter++;
		go.GetComponent<ZombieId> ().zombieId = "Zombie" + counter;
		NetworkServer.Spawn (go);
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
