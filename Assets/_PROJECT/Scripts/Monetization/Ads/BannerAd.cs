using System;
using UnityEngine;
using UnityEngine.Advertisements;
 
public class BannerAd : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(_bannerPosition);

        if (!AccountManager.Instance.CurrentAccount.AdsRemoved)
        {
            ShowBannerAd();
        }

    }

    private void OnDisable()
    {
        HideBannerAd();
    }

    private void LoadBanner(Action callback)
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
        callback?.Invoke();
    }

    private void OnBannerLoaded()
    {
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        LoadBanner(()=> {

            Advertisement.Banner.Show(_adUnitId, options);

        });
    }

    private void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void OnBannerClicked() { }
    private void OnBannerShown() { }
    private void OnBannerHidden() { }
}