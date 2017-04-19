using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputScrollViewMobie : MonoBehaviour
{
	public float speedScale = 0.2f;
	public ScrollView scroll;
	public float deltaStart = 1;
	Vector2 begin;
	float camWith;
	bool scrool = false;

	void Awake ()
	{
		camWith = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && !WasAButton()) {
			scroll.TouchStart ();
			scroll.velocityInput = Vector3.zero;
			begin = Input.mousePosition;
		} else if (Input.GetMouseButton (0)) {
			scroll.velocityInput = new Vector3 ((Input.mousePosition.x - begin.x) * speedScale / camWith,
				(Input.mousePosition.y - begin.y) * speedScale / Camera.main.orthographicSize, 0);
			begin = Input.mousePosition;	
		} else if (Input.GetMouseButtonUp (0) && !WasAButton()) {
			scroll.TouchEnd ();
			scroll.velocityInput = Vector3.zero;
		}
	}

	private bool WasAButton()
	{
		UnityEngine.EventSystems.EventSystem ct
		= UnityEngine.EventSystems.EventSystem.current;

		if (! ct.IsPointerOverGameObject() ) return false;
		if (! ct.currentSelectedGameObject ) return false;
		if (ct.currentSelectedGameObject.tag != "TriggerScroll" )
			return false;

		return true;
	}

}
