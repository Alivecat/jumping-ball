using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public GameObject L_EnemySpawnPoint;
	public GameObject R_EnemySpawnPoint;
	public GameObject Enemy;
	public GameObject currentEnemy;
	public GameObject player;

	public Vector3 SpawnLoc;
	public int L_Or_R;
	public Vector3 pos;

	void Start () {
		player = GameObject.Find("Player");
		StartCoroutine (InsEnemy ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RandomLocSpawn(){
		
		if (L_Or_R == 0) {	
			pos = L_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-1, 8), 0f);
			currentEnemy = Instantiate (Enemy, SpawnLoc, L_EnemySpawnPoint.transform.rotation);
			currentEnemy.tag = "L_enemy";
		} else {
			pos = R_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-8, 8), 0f);
			currentEnemy = Instantiate (Enemy, SpawnLoc, L_EnemySpawnPoint.transform.rotation);
			currentEnemy.tag = "R_enemy";
		}

	}

	IEnumerator InsEnemy(){
		while (true) {
			Debug.Log("EnemySpawn");
			L_Or_R = Random.Range (0, 2);
			RandomLocSpawn ();
			if (player.GetComponent<Player> ().currentPlayerState == Player.PlayerState.SlowerCircle) {
				yield return new WaitForSeconds (1f * 2f);
			} else {
				yield return new WaitForSeconds (1f);
			}

		}

	}

}
