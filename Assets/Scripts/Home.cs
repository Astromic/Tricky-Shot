using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {

	void Start(){
//		PaymentManager.init ();
	}
	public void Play () {
		SceneManager.LoadScene ("Map", LoadSceneMode.Single);
		if(!PlayerPrefs.HasKey ("LevelUnlock"))
		{
			PlayerPrefs.SetInt ("LevelUnlock", 1);
		}
		//PlayerPrefs.SetInt ("LevelUnlock", 150);
	}
	
	// Update is called once per frame
//	void Update () {
//		if(Input.GetKey (KeyCode.Escape))
//		{
//			Application.Quit ();
//		}
//	}

	public void Quit()
	{
		Application.Quit ();
	}
}
