using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;



public class FrontAds : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }


    public void btn()
    {
        RequestInterstitial();

        // 실제 앱에서는 시간이 걸려 안나올 수 도 있음 => 이를 방지하기 위해 기달리기
        StartCoroutine(Show());

        IEnumerator Show()
        {
            while(!this.interstitial.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            // 실행을 위한 show() 호출
            this.interstitial.Show();
            canvas.sortingOrder = -1;
        }
    }

    // 광고가 닫혔을때 실행되는 함수
    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }
}
