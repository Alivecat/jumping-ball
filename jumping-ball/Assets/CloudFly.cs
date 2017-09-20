using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFly : MonoBehaviour {
	public float speed;
	public GameObject player;

	void Update () {
		if (player.GetComponent<Player> ().currentPlayerState == Player.PlayerState.SlowerCircle) {
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
}