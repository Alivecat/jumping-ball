using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour {

    public Camera MainCamera;
    public GameObject[] CloudGroup;
    public GameObject currentCloud;
    public Vector3 SpawnOffset;
    public float spawnTimer;

    int RandomIndex(int min,int max)
    {
        return Random.Range(min, max);
    }

    public IEnumerator InsCloud()
    {
        while (true)
        {
            currentCloud = CloudGroup[RandomIndex(0, 4)];
            if (currentCloud.tag == "L_Cloud")
            {
                SpawnOffset = new Vector3(RandomIndex(-55, -20), 90f, RandomIndex(110, 160));
            }
            if (currentCloud.tag == "R_Cloud")
            {
                SpawnOffset = new Vector3(RandomIndex(20, 55), 90f, RandomIndex(110, 160));
            }

            Instantiate(currentCloud, MainCamera.transform.position + SpawnOffset, Quaternion.identity);

            yield return new WaitForSeconds(spawnTimer);
        }
    }


}
