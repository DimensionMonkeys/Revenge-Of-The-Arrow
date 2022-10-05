using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.Unity;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private Button btnGuest;
    [SerializeField] private Button btnFb;
    [SerializeField] private Button btnGoogle;

    void Start() => AddEvent();

    private void AddEvent()
    {
        btnGuest.onClick.AddListener(GuestLogin);
        btnGoogle.onClick.AddListener(GoogleLogin);
        btnFb.onClick.AddListener(() =>
        {
            FB.Init(FacebookLogin);
        });
    }
    private void GuestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = DeviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    private void GoogleLogin()
    {
        var request = new LoginWithGoogleAccountRequest { CreateAccount = true };
        PlayFabClientAPI.LoginWithGoogleAccount(request, OnLoginSuccess, OnError);
    }

    private void FacebookLogin()
    {
        FB.LogInWithReadPermissions(null, OnFacebookLoggedIn);
    }

    private void OnFacebookLoggedIn(ILoginResult result)
    {
        if (result == null || string.IsNullOrEmpty(result.Error))
        {
            var request = new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString };
            PlayFabClientAPI.LoginWithFacebook(request, OnLoginSuccess, OnError);
        }
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login: " + result.PlayFabId);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error occured");
    }

    // Generate Unique ID when login with guest
    public static string DeviceUniqueIdentifier
    {
        get
        {
            var deviceId = "";


#if UNITY_EDITOR
            deviceId = SystemInfo.deviceUniqueIdentifier + "-editor";
#elif UNITY_ANDROID
                AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
                AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");
                AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
                deviceId = secure.CallStatic<string> ("getString", contentResolver, "android_id");
#elif UNITY_WEBGL
                if (!PlayerPrefs.HasKey("UniqueIdentifier"))
                    PlayerPrefs.SetString("UniqueIdentifier", Guid.NewGuid().ToString());
                deviceId = PlayerPrefs.GetString("UniqueIdentifier");
#else
                deviceId = SystemInfo.deviceUniqueIdentifier;
#endif
            return deviceId;
        }
    }
}
