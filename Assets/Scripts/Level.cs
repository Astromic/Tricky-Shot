using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	private int level;
	public Text textLevel;
	public Sprite lockLevel;
	public Sprite unLockLevel;
	public Image star1, star2, star3;
	public Sprite emptyStar, fullStar;
	private bool lockFlag = false;

	public void SetLevel (int _level)
	{
		//print (PlayerPrefs.GetInt ("LevelUnlock"));
		level = _level;
		textLevel.text = (_level.ToString ());//(level < 10) ? ("0" + level.ToString ()) : (level.ToString ());
		LockLevel (_level > PlayerPrefs.GetInt ("LevelUnlock"));

	}

	void LockLevel (bool lockLeve)
	{
		lockFlag = lockLeve;
		if (lockLeve) {
			GetComponent <Image> ().sprite = lockLevel;
			textLevel.gameObject.SetActive (false);
			star1.enabled = false;
			star2.enabled = false;
			star3.enabled = false;
		} else {
			GetComponent <Image> ().sprite = unLockLevel;
			textLevel.gameObject.SetActive (true);
			star1.enabled = true;
			star2.enabled = true;
			star3.enabled = true;
			SetStar (PlayerPrefs.GetInt ("Star" + level.ToString()));
		}
//		GetComponent <Button> ().enabled = !lockLeve;
	}

	public void SelectLevel ()
	{
		if (!lockFlag) {
			PlayerPrefs.SetInt ("LevelCurrent", level);
			SceneManager.LoadScene ("GamePlay", LoadSceneMode.Single);
		} else {
			Buy ();
		}
	}

	public void Buy(){
//		ShopManager.Instance.BuyProduct (ShopManager.unlockLevel);
//		ShopManager.OnCompletedPayment += ShopManager_OnCompletedPayment;
	}

	void ShopManager_OnCompletedPayment ()
	{
//		ShopManager.OnCompletedPayment -= ShopManager_OnCompletedPayment;
		PlayerPrefs.SetInt ("LevelUnlock", PlayerPrefs.GetInt ("LevelUnlock") + 20);
		MapLevel mp = GameObject.FindObjectOfType<MapLevel> ();
		if (mp != null) {
			mp.Check ();
		}

	}


	public void SetStar (int _index)
	{
		if (_index == 0) {
			star1.sprite = emptyStar;
			star2.sprite = emptyStar;
			star3.sprite = emptyStar;
		} else if (_index == 1) {
			star1.sprite = fullStar;
			star2.sprite = emptyStar;
			star3.sprite = emptyStar;
		} else if (_index == 2) {
			star1.sprite = fullStar;
			star2.sprite = fullStar;
			star3.sprite = emptyStar;
		} else if (_index == 3) {
			star1.sprite = fullStar;
			star2.sprite = fullStar;
			star3.sprite = fullStar;
		}
	}
}
