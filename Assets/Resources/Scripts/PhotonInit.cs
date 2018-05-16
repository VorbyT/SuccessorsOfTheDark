using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInit : Photon.MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1");
        PhotonNetwork.autoJoinLobby = true;
        Debug.Log(1);
    }

    void OnJoinedLobby()
    {
        Debug.Log(1.5);
        PhotonNetwork.JoinOrCreateRoom("IgorianovaZhopa", new RoomOptions(), TypedLobby.Default);
        Debug.Log(2);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("SoDPlayer", new Vector3(0.22f, 0.39f, 2.94f), Quaternion.identity, 0);
        Debug.Log(3);
    }
}
