using UnityEngine;
using System.Collections;

public class BoxControl : MonoBehaviour {
	public Animator anima;
	public TextMesh text;
	// Use this for initialization
	public void Start () {
		text.text = PlayerPrefs.GetInt ("LevelCurrent").ToString ();
		anima.SetBool ("open", true);
		ChangeColor (new Color(137.0f/255, 58.0f/255, 58.0f/255, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TriggerBox()
	{
		//anima.SetBool ("open", true);
	}

	public void SetMaterial(Material frame, Material center, Material cover){
		GameObject[] boxes = GameObject.FindGameObjectsWithTag ("Box");
		boxes [0].GetComponent<Renderer> ().sharedMaterial = frame;
		boxes [1].GetComponent<Renderer> ().sharedMaterial = frame;
		boxes [2].GetComponent<Renderer> ().sharedMaterial = frame;
		boxes [3].GetComponent<Renderer> ().sharedMaterial = center;
		boxes [4].GetComponent<Renderer> ().sharedMaterial = center;
		boxes [5].GetComponent<Renderer> ().sharedMaterial = cover;
		boxes [6].GetComponent<Renderer> ().sharedMaterial = cover;
	}

	public void ChangeColor(Color col){
		GameObject[] boxes = GameObject.FindGameObjectsWithTag ("Box");
		boxes [0].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [1].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [2].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [3].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [4].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [5].GetComponent<Renderer> ().sharedMaterial.color = col;
		boxes [6].GetComponent<Renderer> ().sharedMaterial.color = col;
	}

	public void FinishBox()
	{
		anima.SetBool ("open", false);
		FindObjectOfType <GameManager> ().enabled = false;
		Invoke ("LevelComplete", 1.5f);
	}

	void LevelComplete()
	{
		FindObjectOfType <UiGamePlay>().LevelComplete ();
	}
		
}
