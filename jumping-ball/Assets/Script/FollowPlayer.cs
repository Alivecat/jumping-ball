using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
    public float smoothing;
    public Vector3 targetPos;
    public bool fixation = false;
    public GameManager gameManager;

    void FixedUpdate ()
	{
        if(fixation == false)
        {
            if (player.position.y > transform.position.y)
            {
                targetPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);

            }
        }

        if (player.position.y >= 230f && !gameManager.isEndless)
        {
            targetPos = new Vector3(transform.position.x, 233f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
        }
        

        if(transform.position.y >= 233 && !gameManager.isEndless)
        {
            fixation = true;
        }
		
	}

}
