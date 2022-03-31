using System;
using System.Collections.Generic;

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;

#if FACEBOOK
using Facebook.Unity;
#endif



public class PlayfabFacebookAuthExample : MonoBehaviour
{
    private string _message;
    private string _token;
    public GameObject login;

    public void Start()
    {
#if FACEBOOK

        FB.Init(OnFacebookInitialized, ErrorFacebookInitialized);
        
#endif

    }

    private void OnFacebookInitialized()
    {

        Debug.Log("Facebook intialized");

        //FB.ActivateApp();

        if (PlayerPrefs.HasKey("FBToken"))
        {
            _token = PlayerPrefs.GetString("FBToken");
            SendReuest();
        }

        else if (AccessToken.CurrentAccessToken != null)
        {
            _token = AccessToken.CurrentAccessToken.TokenString;
            SendReuest();
        }
    }


    private void ErrorFacebookInitialized(bool isunityshown)
    {
        Debug.Log("Facebook did not initalized");
    }

    private void SendReuest()
    {
        Debug.Log("Send Request Called");

        PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = _token },
            OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);

        Debug.Log("Send Request Finished");
    }

    public void FacebookLogin()
    {

        var perms = new List<string>() { "public_profile", "email" };


        FB.LogInWithReadPermissions(perms, OnFacebookLoggedIn);

        Debug.Log("Login button called");

    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Clear button called");
    }



    private void OnFacebookLoggedIn(ILoginResult result)
    {

        if (FB.IsLoggedIn)
        {
            Debug.Log("logged in");
        }

        if (result == null || string.IsNullOrEmpty(result.Error))
        {
            //SetMessage("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");
        }
        else
        {
            //SetMessage("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult, true);
        }


        if (result.Cancelled)
        {
            Debug.Log("Cancelled Facebook Login");
        }
        else if (result.Error != null)
        {
            Debug.Log("Error Facebook Login");
        }
        else
        {
            _token = result.AccessToken.TokenString;
            SendReuest();
        }

    }


    private void OnPlayfabFacebookAuthComplete(LoginResult result)
    {

        PlayerPrefs.SetString("FBToken", _token);
        login.SetActive(true);

        //SetMessage("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
    }

    private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    {
        SetMessage("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport(), true);
    }

    public void SetMessage(string message, bool error = false)
    {
        _message = message;
        if (error)
            Debug.LogError(_message);
        else
            Debug.Log(_message);
    }

    public void OnGUI()
    {
        var style = new GUIStyle { fontSize = 40, normal = new GUIStyleState { textColor = Color.white }, alignment = TextAnchor.MiddleCenter, wordWrap = true };
        var area = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Label(area, _message, style);
    }
}