using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SocialPlatforms;

public class AdsControl : MonoBehaviour
{
	public bool PaidVersion = false;
	
	protected AdsControl ()
	{
		
	}

	private BannerView bannerView;
	private static AdsControl _instance;
	InterstitialAd interstitial;

	public string bannerKey, interestitialKey;
	
	public static AdsControl Instance { get {
			return _instance;
		} }
	
	void Awake ()
	{
		
		if (FindObjectsOfType (typeof(AdsControl)).Length > 1) {
			Destroy (gameObject);
			return;
		}
		
		_instance = this;
		RequestBannerBottom ();
		ShowBanner ();
		MakeNewAdmobAd ();
		
		DontDestroyOnLoad (gameObject); //Already done by CBManager
	}
	
	public void HandleAdClosed (object sender, EventArgs args)
	{
		
		if (interstitial != null)
			interstitial.Destroy ();
		MakeNewAdmobAd ();
		
	}
	
	void MakeNewAdmobAd ()
	{
#if UNITY_ANDROID
		interstitial = new InterstitialAd (interestitialKey);
#endif
#if UNITY_IPHONE
		interstitial = new InterstitialAd (interestitialKey);
#endif
		interstitial.OnAdClosed += HandleAdClosed;
		AdRequest request = new AdRequest.Builder ().Build ();
		interstitial.LoadAd (request);
	}
	
	public void showAds ()
	{
		//Debug.Log ("Show ad");
		if (interstitial != null && interstitial.IsLoaded () && !PaidVersion) 
			interstitial.Show ();
	}

	public void RequestBannerTop ()
	{
		if(bannerView!=null)
			bannerView.Destroy ();
		string adUnitId = bannerKey;
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Load a banner ad.
		AdRequest request = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (request);
	}

	public void RequestBannerBottom ()
	{
		if(bannerView!=null)
		bannerView.Destroy ();
		string adUnitId = bannerKey;
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Load a banner ad.
		AdRequest request = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (request);
	}
	public void ShowBanner()
	{
		if(!PaidVersion)
		bannerView.Show ();
	}

}

