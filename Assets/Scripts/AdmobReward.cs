using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobReward : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
    int Undo;
    int Reward = 1;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        rewardBasedVideo = RewardBasedVideoAd.Instance;



        rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
        rewardBasedVideo.OnAdFailedToLoad += RewardBasedVideo_OnAdFailedToLoad;



    }

    private void RewardBasedVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        GameManager._Instance.Sorry();
    }


    public void RequestRewardAds()
    {


        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, "ca-app-pub-6411957783425861/6692109707");

        ShowAds();

    }

    private void RewardBasedVideo_OnAdRewarded(object sender, Reward e)
    {
        Undo = PlayerPrefs.GetInt("Undo");
        Undo += Reward;
        PlayerPrefs.SetInt("Undo", Undo);
        GameManager._Instance.LoadVariable();
    }

    void ShowAds()
    {
        while (!rewardBasedVideo.IsLoaded())


            rewardBasedVideo.Show();
        GameManager._Instance.Continue();

    }
}
