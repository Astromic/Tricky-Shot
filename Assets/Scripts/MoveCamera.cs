using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public Vector2 limitCam;
	public float speedCam = 0.1f;
	public float minDeltaTouch = 0.2f;
	public GameManager gameManager;
	float startMouse;
	float deltaMouse;
	float currenCameraPositionY;
	const float DeltaCam = 10;
	// Use this for initialization
	void OnEnable () {
		currenCameraPositionY = transform.position.y;
		deltaMouse = 0;
		startMouse = Input.mousePosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton (0))
		{
			//MoveMouseOn ();
			//startMouse = Input.mousePosition.y;
		}
		else if(Input.GetMouseButtonUp (0))
		{
			//EndMouse ();
		}
		else
		{
			//MoveMouseUp ();
		}
	}

	void MoveMouseOn()
	{
		#if UNITY_EDITOR
		deltaMouse = Input.mousePosition.y - startMouse;
		#else
		deltaMouse = Input.GetTouch (0).deltaPosition.y;
		#endif
		if(Mathf.Abs (deltaMouse) > 0.3f)
		transform.position = new Vector3 (transform.position.x,
			transform.position.y + deltaMouse * speedCam,
			transform.position.z);
	}

	void MoveMouseUp()
	{
		if(Mathf.Abs (transform.position.y - currenCameraPositionY) > 0.1f)
		{
			transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, currenCameraPositionY, 0.2f), transform.position.z);
		}
		else
		{
			transform.position = new Vector3 (transform.position.x, currenCameraPositionY, transform.position.z);
			GameManager.level = ((int)limitCam.y - (int)currenCameraPositionY) / 10 + 1;
			gameManager.box = gameManager.transform.GetChild (GameManager.level - 1).GetComponentInChildren <BoxControl> ();
			this.enabled = false;
		}
	}

	void EndMouse()
	{
		if(deltaMouse > minDeltaTouch)
		{
			currenCameraPositionY -= DeltaCam;
		}
		else if(deltaMouse < -minDeltaTouch)
		{
			currenCameraPositionY += DeltaCam;
		}
		currenCameraPositionY = Mathf.Clamp (currenCameraPositionY, limitCam.x, limitCam.y);
	}
}
