using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private Button btnGuest;
    [SerializeField] private Button btnFb;
    [SerializeField] private Button btnGoogle;

    void Start() => AddEvent();

    private void AddEvent()
    {
        btnGuest.onClick.AddListener(GuestLogin);
    }
    private void GuestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = DeviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginGuestSuccess, OnError);
    }

    private void OnLoginGuestSuccess(LoginResult result)
    {
        Debug.Log("Login as Guest: " + result.PlayFabId);
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
