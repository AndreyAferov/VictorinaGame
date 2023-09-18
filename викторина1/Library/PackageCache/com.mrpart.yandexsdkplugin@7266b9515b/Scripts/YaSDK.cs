using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEditor;
namespace YandexSDK
{
    public enum Platform
    {
        phone,
        desktop
    }
    public class YaSDK : MonoBehaviour
    {
        [DllImport("__Internal")] private static extern void Authenticate();
        [DllImport("__Internal")] private static extern void SetPlayerData(string data);
        [DllImport("__Internal")] private static extern void GetPlayerData();
        [DllImport("__Internal")] private static extern void ShowFullscreenAd();
        [DllImport("__Internal")] private static extern void OpenRateUs();
        [DllImport("__Internal")] private static extern int ShowRewardedAd(string placement);

        public static YaSDK instance;
        public delegate void onPlayerAuthenticatedHandler();
        public static event onPlayerAuthenticatedHandler onPlayerAuthenticated;
        public delegate void onGetPlayerDataHandler(string item);
        public static event onGetPlayerDataHandler onGetPlayerData;
        public event Action onInterstitialShown;
        public event Action<string> onInterstitialFailed;
        public event Action<int> onRewardedAdOpened;
        public static event Action<string> onRewardedAdReward;
        public static event Action<int> onRewardedAdClosed;
        public static event Action<int> onRewardedAdError;
        public bool isInterstitialReady = false;

        public Platform currentPlatform;
        
        public bool canReview = false;
        public int rewardedAdPlacementAsInt = 0;
        public string rewardedAdPlacement = string.Empty;

        [SerializeField] private int secondTillNextInterstitial = 180;
#if UNITY_EDITOR
        private GameObject rewardedAdPrefab;
        private GameObject interstitialAdPrefab;
#endif

        private float currentSecondsTillNextInterstitial;

        public void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            currentSecondsTillNextInterstitial = secondTillNextInterstitial;
            StartCoroutine(CountTillNextInterstitial());
        }

        public void AuthenticateUser()
        {
            Authenticate();
        }
        public void OnCanReview(string str){
            canReview = (str == "yes")? true : false;
        }
        public void OnPlayerAuthenticated()
        {
            onPlayerAuthenticated?.Invoke();
        }
        public void SetSave<T>(T saveStateClass)
        {
            string dataStr = JsonUtility.ToJson(saveStateClass);
            SetPlayerData(dataStr);
        }
        public void GetSave()
        {
            GetPlayerData();
        }
        public void OnGetPlayerData(string dataStr)
        {
            if (!dataStr.Contains("none"))
            {
                onGetPlayerData?.Invoke(dataStr);
            }
            else
            {
                onGetPlayerData?.Invoke(string.Empty);
            }
        }
        public void OnGetPlayerPlatform(string p)
        {
            switch (p)
            {
                case "phone":
                    currentPlatform = Platform.phone;
                    break;
                case "desktop":
                    currentPlatform = Platform.desktop;
                    break;
            }
        }
        public void ShowInterstitial()
        {
            if (!isInterstitialReady)
            {
                return;
            }
            AudioListener.pause = true;
            isInterstitialReady = false;
            Time.timeScale = 0;
#if UNITY_EDITOR
            interstitialAdPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Packages/com.mrpart.yandexsdkplugin/Prefab/InterstitialAd.prefab", typeof(GameObject));
            Instantiate(interstitialAdPrefab);
#else
            ShowFullscreenAd();
#endif


        }

        public void ShowRewarded(string placement)
        {
#if UNITY_EDITOR
            rewardedAdPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Packages/com.mrpart.yandexsdkplugin/Prefab/RewardedAd.prefab", typeof(GameObject));
            RewardedAdEditor reawrded = Instantiate(rewardedAdPrefab).GetComponent<RewardedAdEditor>();

            reawrded.placement = placement;
#else
            rewardedAdPlacementAsInt = (ShowRewardedAd(placement));
#endif
            rewardedAdPlacement = (placement);
            AudioListener.pause = true;
        }

        public void OnInterstitialShown()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            StartCoroutine(CountTillNextInterstitial());
            if (onInterstitialShown != null)
            {
                onInterstitialShown();
            }
        }

        public void OnInterstitialError(string error)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            StartCoroutine(CountTillNextInterstitial());
            if (onInterstitialFailed != null)
            {
                onInterstitialFailed(error);
            }
        }

        public void OnRewardedOpen(int placement)
        {
            Time.timeScale = 0;
            if (onRewardedAdOpened != null)
            {
                onRewardedAdOpened(placement);
            }
        }
#if UNITY_EDITOR
        public void OnRewarded(string placement)
        {
            if (placement == rewardedAdPlacement)
            {
                if (onRewardedAdReward != null)
                {
                    onRewardedAdReward?.Invoke(rewardedAdPlacement);
                }
            }
        }
#endif
        public void OnRewarded(int placement)
        {
            if (placement == rewardedAdPlacementAsInt)
            {
                if (onRewardedAdReward != null)
                {
                    onRewardedAdReward?.Invoke(rewardedAdPlacement);
                }
            }
        }

        public void OnRewardedClose(int placement)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            if (onRewardedAdClosed != null)
            {
                onRewardedAdClosed(placement);
            }
        }

        public void OnRewardedError(int placement)
        {

            Time.timeScale = 1;
            AudioListener.pause = false;
            if (onRewardedAdError != null)
            {
                onRewardedAdError(placement);
            }
        }
        public void OpenRateUsWindow()
        {
            OpenRateUs();
            canReview = false;
        }
        private IEnumerator<WaitForSeconds> CountTillNextInterstitial()
        {
            while (currentSecondsTillNextInterstitial > 0)
            {
                currentSecondsTillNextInterstitial -=  Time.unscaledDeltaTime;
                yield return null;
            }
            isInterstitialReady = true;
            currentSecondsTillNextInterstitial = secondTillNextInterstitial;
        }
    }
}
