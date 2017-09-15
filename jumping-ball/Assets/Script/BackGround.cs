﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
	public float speed;
    public float offset;
	public GameObject player;

    void Start()
    {
        offset = 38.4f; //背景图片的高度
    }
	void Update()
	{
		
		if (player.transform.position.y > (transform.position.y + offset))
		{
			transform.position += new Vector3(0, 38.4f * 2, 0);
		}
	}
}
