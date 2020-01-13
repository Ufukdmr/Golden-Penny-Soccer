using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobInterstitial : MonoBehaviour
{
    InterstitialAd interstitial;

    static AdmobInterstitial admobCont;

    int repetition;

    void Start()
    {

        if (admobCont == null)
        {
            DontDestroyOnLoad(gameObject);
            admobCont = this;

            MobileAds.Initialize(initStatus => { });

            RequestInterstitial();

            //  AdRequest request = new AdRequest.Builder()
            //.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
            //.Build();
            //  this.interstitial.LoadAd(request);

            AdRequest request = new AdRequest.Builder()
        .Build();
            this.interstitial.LoadAd(request);

        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void AdmobShow()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();

        }
        admobCont = null;
        Destroy(gameObject);

    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6411957783425861/3465334655";
#elif UNITY_IPHONE
                                            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
    }
}
