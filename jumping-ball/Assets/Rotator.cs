using UnityEngine;

public class Rotator : MonoBehaviour {

	public float speed;


    void Start()
    {
        RandomSpeed();
    }

	void Update () {
		transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}

    void RandomSpeed()
    {
        speed = Random.Range(80, 220);
    }

}
