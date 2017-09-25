using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

    public float HP = 10;
    public float attackTime;
    public GameObject sting;
    public SpriteRenderer BossEye;
    public Player playerCom;


    private void Start()
    {
        playerCom = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sting")
        {
            HP--;
            sting.gameObject.SetActive(false);
            playerCom.setToNormalTrigger = true;
            StartCoroutine(closeEye());
        }
    }

    IEnumerator closeEye()
    {
        BossEye.enabled = false;
        yield return new WaitForSeconds(1f);
        BossEye.enabled = true;
    }
}
