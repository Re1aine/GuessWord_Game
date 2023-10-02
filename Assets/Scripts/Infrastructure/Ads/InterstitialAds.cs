using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : IUnityAdsShowListener, IUnityAdsLoadListener 
{
    private const string AndroidAdUnitId = "Interstitial_Android";
    private const string IOSAdUnitId = "Interstitial_iOS";

    private readonly string _adUnitId;

    public InterstitialAds()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSAdUnitId
            : AndroidAdUnitId;
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Showing Interstitial Ad: " + _adUnitId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Interstitial Ad Completed");
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Loaded: " + _adUnitId);
        ShowAd();
       
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }
    
    public void LoadAd()
    {
        Debug.Log("Loading Interstitial Ad: " + _adUnitId);
        
        Advertisement.Load(_adUnitId, this);
        
        OnUnityAdsAdLoaded(_adUnitId);
    }
    
    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
}