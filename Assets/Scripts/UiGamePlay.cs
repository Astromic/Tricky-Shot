using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UiGamePlay : MonoBehaviour
{
	public Animator anima;
	public GameManager gameManager;
	public Image star1, star2, star3;
	public Sprite empty_star, full_star;

	void Start(){
		PlayerPrefs.SetInt ("Timer", 0);
	}

	public void Pause ()
	{
		anima.SetBool ("pause", true);
		Time.timeScale = 0;
	}

	public void Next ()
	{
		Time.timeScale = 1;
		gameManager.enabled = true;
		PlayerPrefs.SetInt ("LevelCurrent", PlayerPrefs.GetInt ("LevelCurrent") + 1);
		//SceneManager.LoadScene ("Level" + PlayerPrefs.GetInt ("LevelCurrent").ToString (),LoadSceneMode.Single);
		gameManager.InstanceMap ();
		Resume ();
	}

	public void Back ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("Map", LoadSceneMode.Single);
	}

	public void LevelComplete ()
	{
		//Debug.Log ("complete");
		ShowAds ();
		Time.timeScale = 0;
		anima.SetBool ("complete", true);
		if (PlayerPrefs.GetInt ("LevelCurrent") >= PlayerPrefs.GetInt ("LevelUnlock")) {
			PlayerPrefs.SetInt ("LevelUnlock", PlayerPrefs.GetInt ("LevelCurrent") + 1);
		}
		if (GameManager.shootTotal < 5) {

			star1.sprite = full_star;
			star2.sprite = full_star;
			star3.sprite = full_star;
			PlayerPrefs.SetInt ("Star" + PlayerPrefs.GetInt ("LevelCurrent").ToString (),3);
			
		} else if (GameManager.shootTotal >= 5 && GameManager.shootTotal < 10) {

			star1.sprite = full_star;
			star2.sprite = full_star;
			star3.sprite = empty_star;
			PlayerPrefs.SetInt ("Star" + PlayerPrefs.GetInt ("LevelCurrent").ToString (),2);
			
		} else if (GameManager.shootTotal >= 10) {

			star1.sprite = full_star;
			star2.sprite = empty_star;
			star3.sprite = empty_star;
			PlayerPrefs.SetInt ("Star" + PlayerPrefs.GetInt ("LevelCurrent").ToString (),1);

		}
		GameManager.shootTotal = 0;
	}

	public void Resume ()
	{
		Time.timeScale = 1;
		anima.SetBool ("pause", false);
		anima.SetBool ("complete", false);
	}

	void ShowAds ()
	{
		int timeAds = PlayerPrefs.GetInt ("Timer");
		if (timeAds >= 2) {
			timeAds = 0;
			AdsControl.Instance.showAds ();
		} else
			timeAds++;
		PlayerPrefs.SetInt ("Timer", timeAds);
	}
}
