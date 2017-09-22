using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCloud : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        CloudInitialize();

    }
	
	// Update is called once per frame
	void Update () {
        if(gameObject.tag == "L_Cloud")
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        if (gameObject.tag == "R_Cloud")
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    void CloudInitialize()
    {
        speed = Random.Range(10f, 15f);
    }

    

    void OnTriggerExit2D(Collider2D col)
    {
        CloudInitialize();
        if (gameObject.tag == "L_Cloud")
            transform.position = new Vector3(-10f, Random.Range(2f, 8f), 80f);
        if (gameObject.tag == "R_Cloud")
            transform.position = new Vector3(9f, Random.Range(2f, 8f), 80f);

    }


         
    
}
