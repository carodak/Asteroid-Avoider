using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;
    /*
        Make a singleton
        From another script we can call AdManager.Instance 
        and get a reference to this instance
    */
    public static AdManager Instance;

#if UNITY_ANDROID
    private string gameId = "4878777";
    private string rewarded = "Rewarded_Android";
#elif UNITY_IOS
    private string gameId = "4878776";
    private string rewarded = "Rewarded_iOS";
#endif

    private GameOverHandler gameOverHandler;

    void Awake(){ //called just slightly before the Start
    // When we return back to the main menu and there is another instance in the scene that is not us, it will be destroyed
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject); // when we change scenes this object won't be destroyed
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
   }

    public void ShowAd(GameOverHandler gameOverHandler){
        Debug.Log("Showing ad");
        this.gameOverHandler = gameOverHandler;
        Advertisement.Show(rewarded);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult){
            case ShowResult.Finished:
            gameOverHandler.ContinueGame();
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ads Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ads Ready");
    }
   
}
