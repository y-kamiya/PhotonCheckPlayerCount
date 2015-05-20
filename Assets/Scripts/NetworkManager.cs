using System;
using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

    public bool NotJoinRoom = false;
    public string TargetAppVersion = "1.1";
    public string TargetAppVersionPun = "1.25";
    public string RoomName = "room1";
    public string PlayerName = "defaultPlayer";
    public string EncryptedString;

    void Awake () {
        PhotonNetwork.ConnectUsingSettings(TargetAppVersion);
        if (!String.IsNullOrEmpty(TargetAppVersionPun))
        {
            PhotonNetwork.versionPUN = TargetAppVersionPun;
        }
    }
 
    void Update () {
    }
  
    //ロビー参加成功時のコールバック
    void OnJoinedLobby() {
        if (!this.NotJoinRoom)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.SetAuthParameters(this.PlayerName, this.EncryptedString);
            PhotonNetwork.playerName = this.PlayerName;

            PhotonNetwork.JoinOrCreateRoom(this.RoomName, new RoomOptions(), new TypedLobby());
        }
    }
 
    //ルーム参加成功時のコールバック
    void OnJoinedRoom() {
        Debug.Log("ルームへの参加に成功しました");
        // PhotonNetwork.room.open = false;
        // Debug.Log(PhotonNetwork.room.visible);
        
    }
 
    void OnGUI() {
        //サーバーとの接続状態をGUIへ表示
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connectionStateDetailed == PeerState.JoinedLobby)
        {
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();
            if (roomList.Length == 0)
            {
                GUILayout.Label("there is no room");
            }
            foreach(RoomInfo info in roomList)
            {
                GUILayout.Label("player count of " + info.name + ": " + info.playerCount.ToString());
            }
        } 
        else if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            GUILayout.Label("player count in room( " + PhotonNetwork.room.ToString() + "): " + PhotonNetwork.playerList.Length.ToString());
        }

    }
}
