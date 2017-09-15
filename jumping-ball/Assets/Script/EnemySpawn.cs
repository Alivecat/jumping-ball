using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public GameObject L_EnemySpawnPoint;
	public GameObject R_EnemySpawnPoint;
    [Space]
	public GameObject[] Enemy;
    [Space]
	public GameObject currentEnemy;
	public GameObject player;

	public Vector3 SpawnLoc;
	public int L_Or_R;
	public Vector3 pos;
    public float spawnTimer;

	void Start () {
		player = GameObject.Find("Player");
        //间隔spawnTimer时间生成小怪
        spawnTimer = 1f;
        StartCoroutine (InsEnemy ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RandomLocSpawn(){
		
		if (L_Or_R == 0) {	
			pos = L_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-1, 8), 0f);
			currentEnemy = Instantiate (Enemy[RandomIndex()], SpawnLoc, L_EnemySpawnPoint.transform.rotation);
            currentEnemy.transform.Find("tag").tag = "L_enemy";
		} else {
			pos = R_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-8, 8), 0f);
			currentEnemy = Instantiate (Enemy[RandomIndex()], SpawnLoc, L_EnemySpawnPoint.transform.rotation);
            currentEnemy.transform.Find("tag").tag = "R_enemy";
        }

	}

	IEnumerator InsEnemy(){
		while (true) {
			Debug.Log("EnemySpawn");
			L_Or_R = Random.Range (0, 2); //0 -> left 1-> right
			RandomLocSpawn ();
			if (player.GetComponent<Player> ().currentPlayerState == Player.PlayerState.SlowerCircle) {
				yield return new WaitForSeconds (spawnTimer * 2f);
			} else {
				yield return new WaitForSeconds (spawnTimer);
			}

		}

	}

    int RandomIndex()
    {
        return Random.Range(0, 4);
    }

}
