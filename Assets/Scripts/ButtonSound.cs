using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonSound : MonoBehaviour{
	public Image buttonSound;
	public Sprite sound;
	public Sprite unSound;

	void OnEnable()
	{
		if(!PlayerPrefs.HasKey ("sound"))
		{
			AudioListener.volume = 1;
			AudioListener.pause = false;
			buttonSound.sprite = sound;
			PlayerPrefs.SetInt ("sound", 1);
		}
		else
		{
			ButtonClick ();
			ButtonClick ();
		}
		AudioListener.pause = false;
		if(buttonSound == null)
		{
			buttonSound = GetComponent <Image> ();
		}
		if(AudioListener.volume == 0 || AudioListener.pause)
		{
			buttonSound.sprite = unSound;
		}
		else
		{
			buttonSound.sprite = sound;
		}
	}

	public void ButtonClick()
	{
		if(buttonSound.sprite == unSound)
		{
			AudioListener.volume = 1;
			AudioListener.pause = false;
			buttonSound.sprite = sound;
			PlayerPrefs.SetInt ("sound", 1);
		}
		else
		{
			AudioListener.volume = 0;
			AudioListener.pause = true;
			buttonSound.sprite = unSound;
			PlayerPrefs.SetInt ("sound", 0);
		}
	}
}
