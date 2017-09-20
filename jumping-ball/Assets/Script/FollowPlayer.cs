using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
    public float smoothing;
    public Vector3 targetPos;

	void FixedUpdate ()
	{
		if (player.position.y > transform.position.y)
		{
            targetPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
           
        }
	}

}
