﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFly : MonoBehaviour {
	public float speed;
    public Player playerCom;

    private void Start()
    {
        speed = Random.Range(1f, 3f);
        playerCom = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(CloudDie());
    }

    void Update () {
		if (playerCom.GetComponent<Player>().currentPlayerState == Player.PlayerState.SlowerCircle) {
			if (gameObject.tag == "L_Cloud") {
				transform.Translate ((speed * 0.5f) * Time.deltaTime, 0f, 0f);
			}
			if (gameObject.tag == "R_Cloud") {
				transform.Translate ((-speed * 0.5f) * Time.deltaTime, 0f, 0f);
			}

		} else {
			if (gameObject.tag == "L_Cloud") {
				transform.Translate (speed * Time.deltaTime, 0f, 0f);
			}
			if (gameObject.tag == "R_Cloud") {
				transform.Translate (-speed * Time.deltaTime, 0f, 0f);
			}
		}
	}

    IEnumerator CloudDie()
    {
        yield return new WaitForSeconds(100f);
        GameObject.Destroy(gameObject);
    }
}