using System;
using System.Collections.Generic;

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;
using TMPro;

#if FACEBOOK
using Facebook.Unity;
#endif



public class PlayfabFacebookAuthExample : MonoBehaviour
{
    private string _message;
    private string _token;
    public GameObject login;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;
    public TextMeshProUGUI text6;
    public TextMeshProUGUI text7;
    public TextMeshProUGUI text8;
    public TextMeshProUGUI text9;
    public TextMeshProUGUI text10;
    public TextMeshProUGUI text11;







    public void Start()
    {
#if FACEBOOK

        FB.Init(OnFacebookInitialized, ErrorFacebookInitialized);
        
#endif

    }

    private void OnFacebookInitialized()
    {

        Debug.Log("Facebook intialized");
        text.text = "Facebook intialized";

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
        text1.text = "Facebook did not initalized";
    }

    private void SendReuest()
    {
        Debug.Log("Send Request Called");
        text2.text = "Send Request Called";

        PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = _token },
            OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);

        //PlayFabClientAPI.LoginWithPlayFab();

        Debug.Log("Send Request Finished");
        text3.text = "Send Request Finished";
    }

    public void FacebookLogin()
    {


        Debug.Log("Login button called");

        text4.text = "Login button called";

        var perms = new List<string>() { "public_profile", "email" };


        FB.LogInWithReadPermissions(perms, OnFacebookLoggedIn);



    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Clear button called");
        text5.text = "Clear button called";
    }



    private void OnFacebookLoggedIn(ILoginResult result)
    {

        if (FB.IsLoggedIn)
        {
            Debug.Log("logged in");
            text6.text = "logged in";
        }

        if (result == null || string.IsNullOrEmpty(result.Error))
        {
            text7.text = "Facebook Auth Complete! Access Token: \" + AccessToken.CurrentAccessToken.TokenString + \"\\nLogging into PlayFab...";
            //SetMessage("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");
        }
        else
        {

            text8.text = "Facebook Auth Failed: \" + result.Error + \"\\n\" + result.RawResult, true";

            //SetMessage("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult, true);
        }


        if (result.Cancelled)
        {
            Debug.Log("Cancelled Facebook Login");
            text9.text = "Cancelled Facebook Login";
        }
        else if (result.Error != null)
        {
            Debug.Log("Error Facebook Login");
            text10.text = "Error Facebook Login";
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
        text11.text = "PlayFab Facebook Auth Failed ";
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