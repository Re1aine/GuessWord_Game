using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private Button _button;

    public event Action RewardAdsComplete;
    
    private const string AndroidAdUnitId = "Rewarded_Android";
    private const string IOSAdUnitId = "Rewarded_iOS";


    public bool _isRaycastable = true;
    private string _adUnitId;
    private bool _isShowing;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSAdUnitId
            : AndroidAdUnitId;
    }

    private void Start()
    {
       LoadAd();
    }
    
    private void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
        OnUnityAdsAdLoaded(_adUnitId);
    }
    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            _button.onClick.AddListener(ShowAd);
            _button.interactable = true;
        }
    }
    
    private void ShowAd()
    {
        if(!_isRaycastable)
            return;
        
        _button.interactable = false;
        
        Advertisement.Show(_adUnitId, this);
        _isShowing = true;
    }
    
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            _isShowing = false;
            RewardAdsComplete?.Invoke();
        }
    }
    
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        _button.interactable = true;

        StartCoroutine(HandleAdsShowClick());
    }
    
    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Click on ads");
    }
    
    private IEnumerator HandleAdsShowClick()
    {
        while (_isShowing)
        {
            yield return null;
            
            if (Input.GetMouseButtonDown(0)) 
                OnUnityAdsShowClick(_adUnitId);
        }
    }
 
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    
    public void StartCheckoutWord()
    {
        _isRaycastable = false;
    }

    public void CompleteCheckoutWord()
    {
        _isRaycastable = true;
    }
}