using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public GameObject L_EnemySpawnPoint;
	public GameObject R_EnemySpawnPoint;
    [Space]
	public GameObject[] Enemy_L;
    public GameObject[] Enemy_R;
    [Space]
	public GameObject currentEnemy;
	public GameObject player;

	public Vector3 SpawnLoc;
	public int L_Or_R;
	public Vector3 pos;
    public float spawnTimer;

    public int min;
    public int max;

	void Start () {
        min = 0;
        max = 4;
        player = GameObject.Find("Player");
        //间隔spawnTimer时间生成小怪
        spawnTimer = 1f;
	}
	

	void RandomLocSpawn(){
        //0 -> left 1-> right
        if (L_Or_R == 0) {	
			pos = L_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-1, 8), pos.z);
            
            currentEnemy = Instantiate(Enemy_L[RandomIndex(min,max)], SpawnLoc, L_EnemySpawnPoint.transform.rotation);

            currentEnemy.transform.Find("tag").tag = "L_enemy";
		} else {
			pos = R_EnemySpawnPoint.transform.position;
			SpawnLoc = new Vector3 (pos.x, pos.y + Random.Range (-8, 8), pos.z);
            
            currentEnemy = Instantiate (Enemy_R[RandomIndex(min,max)], SpawnLoc, L_EnemySpawnPoint.transform.rotation);
            currentEnemy.transform.Find("tag").tag = "R_enemy";
            
        }

	}
    //GameManager中开始协程
	public IEnumerator InsEnemy(){
		while (true) {
			L_Or_R = Random.Range (0, 2); //0 -> left 1-> right
			RandomLocSpawn ();
			if (player.GetComponent<Player> ().currentPlayerState == Player.PlayerState.SlowerCircle) {
                
                //SlowerCircle状态下怪物生成速度减半
                yield return new WaitForSeconds (spawnTimer * 2f);
			} else {
				yield return new WaitForSeconds (spawnTimer);
			}

		}

	}

    int RandomIndex(int min, int max)
    {
        return Random.Range(min, max);
    }

}
