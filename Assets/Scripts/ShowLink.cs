using UnityEngine;
using System.Collections;

public class ShowLink : MonoBehaviour {

	public string urlLink;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenUrl()
	{
		Application.OpenURL (urlLink);
	}
}
