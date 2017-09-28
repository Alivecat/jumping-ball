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
            StartCoroutine(playerCom.SetToNormal(0f));
            StartCoroutine(closeEye());
        }

        if(HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator closeEye()
    {
        BossEye.enabled = false;
        yield return new WaitForSeconds(1f);
        BossEye.enabled = true;
    }
}
