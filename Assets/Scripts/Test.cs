using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	public bool move = false;
	Vector2 velocity = Vector2.one;

	// Use this for initialization
	public void SetStart (Vector3 velo) {
		move = true; 
		velocity = new Vector2 (velo.x, velo.y);
	}
	
	// Update is called once per frame
	void Update () {
		if(move)
		{
			transform.position = new Vector3 (transform.position.x + velocity.x * Time.deltaTime,
				transform.position.y + velocity.y * Time.deltaTime + Physics.gravity.y * 0.5f * Time.deltaTime * Time.deltaTime,
				transform.position.z);
			velocity = new Vector2 (velocity.x, velocity.y + Time.deltaTime * Physics.gravity.y);
		}
	}
}
