using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCloud : MonoBehaviour {

    public float speed;
    public float min;
    public float max;
    public float L_SpawnPoint;
    public float R_SpawnPoint;
	// Use this for initialization
	void Start () {
        CloudInitialize(min,max);

    }
	
	// Update is called once per frame
	void Update () {
        if(gameObject.tag == "L_Cloud")
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        if (gameObject.tag == "R_Cloud")
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    void CloudInitialize(float min,float max)
    {
        speed = Random.Range(min, max);
    }

    

    void OnTriggerExit2D(Collider2D col)
    {
        CloudInitialize(min,max);
        if (gameObject.tag == "L_Cloud")
            transform.position = new Vector3(L_SpawnPoint, Random.Range(2f, 8f), 0f);
        if (gameObject.tag == "R_Cloud")
            transform.position = new Vector3(R_SpawnPoint, Random.Range(2f, 8f), 0f);

    }


         
    
}
