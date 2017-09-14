using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
	public float speed;
	public GameObject player;

	void Update()
	{
		
		if (player.transform.position.y > (transform.position.y + 38.4f))
		{
			transform.position += new Vector3(0, 38.4f * 2, 0);
		}
	}
}
