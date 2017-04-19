using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoSetScollView : MonoBehaviour {
	public ScrollView scrollView;
	public GameObject content;
	// Use this for initialization
	void OnEnable () {
		for(int i= 0; i < content.transform.childCount; i++)
		{
			content.transform.GetChild (i).localPosition = new Vector3(scrollView.scrollX.deltaFix * i, content.transform.localPosition.y, content.transform.localPosition.z);
		}
		scrollView.scrollX.deltaScroll = new Vector2 (-scrollView.scrollX.deltaFix * (content.transform.childCount - 1), 0);
	}
	#if UNITY_EDITOR
	void Update()
	{
		if(scrollView != null && content != null)
			OnEnable ();
	}
	#endif

}
