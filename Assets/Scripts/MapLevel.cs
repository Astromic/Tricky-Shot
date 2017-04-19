using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MapLevel : MonoBehaviour
{
	public GameObject left;
	public GameObject right;
	private int currentPage;
	public int maxPage;

	public GameObject[] item;

	void OnEnable ()
	{
//		PlayerPrefs.DeleteAll ();
		currentPage = 0;
		Check ();
		AdsControl.Instance.RequestBannerBottom ();
		AdsControl.Instance.ShowBanner ();
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Check ();
		if (currentPage == 0) {
			
			left.SetActive (false);
			right.SetActive (true);

		} else if (currentPage == maxPage -1) {

			left.SetActive (true);
			right.SetActive (false);
		} else {
			left.SetActive (true);
			right.SetActive (true);
		}
	}

	public void Back ()
	{
		SceneManager.LoadScene ("Home", LoadSceneMode.Single);
	}

	public void Left ()
	{
		if (currentPage > 0) {
			currentPage--;
			Check ();
		}
		
	}

	public void Right ()
	{
		if (currentPage < maxPage - 1) {
			currentPage++;
			Check ();
		}
		
	}


	public void Check ()
	{
		for (int i = 0; i < 10; i++) {
			item [i].GetComponent<Level> ().SetLevel (10 * currentPage + i + 1);
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
		Check ();
	}
}
