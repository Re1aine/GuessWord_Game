using UnityEngine;
using UnityEngine.Advertisements;

public class AdsService : IAdsService, IUnityAdsInitializationListener
{
    private const string AndroidGameId = "5405355";
    private const string IOSGameId = "5405354";

    private readonly bool _testMode = true;
    private string _gameId;

    private InterstitialAds _interstitialAds;
    
    public AdsService()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSGameId
            : AndroidGameId;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, _testMode, this);

        _interstitialAds = new InterstitialAds();
    }
    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }


    public void LoadInterstitialAds()
    {
        _interstitialAds.LoadAd();
    }

    public void ShowInterstitialAds()
    {
        _interstitialAds.ShowAd();
    }
    
}