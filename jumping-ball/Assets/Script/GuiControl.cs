using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiControl : MonoBehaviour {
    public Text Score;
    public Text State;
	public Slider HPbar;
	public Player player;
	public GameObject playerObj;
	public Transform mainCameraTran;

	private Transform playTran;

	public float speed;

    void Start () {
		mainCameraTran = GameObject.Find ("Main Camera").transform;
		playTran = playerObj.transform;
		HPbar.value = player.HP;
	}


	void Update () {
		HPbar.value = Mathf.Lerp(HPbar.value, player.HP/6f, speed * Time.deltaTime);
		Score.text = (mainCameraTran.position.y).ToString("F2") + " M";

	}

}
