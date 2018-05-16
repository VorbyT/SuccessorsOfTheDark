﻿using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabAuthenticator : MonoBehaviour
{

    private string _playFabPlayerIdCache;

    public void Awake()
    {
        AuthenticateWithPlayFab();
    }

    
    private void AuthenticateWithPlayFab()
    {
        LogMessage("PlayFab authenticating using Custom ID...");

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = PlayFabSettings.DeviceUniqueIdentifier
        }, RequestPhotonToken, OnPlayFabError);
    }

   
    private void RequestPhotonToken(LoginResult obj)
    {
        LogMessage("PlayFab authenticated. Requesting photon token...");

        
        _playFabPlayerIdCache = obj.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID
        }, AuthenticateWithPhoton, OnPlayFabError);
    }


    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };

        
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);  

       
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        
        PhotonNetwork.AuthValues = customAuth;
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());
    }

    public void LogMessage(string message)
    {
        Debug.Log("PlayFab + Photon Example: " + message);
    }
}