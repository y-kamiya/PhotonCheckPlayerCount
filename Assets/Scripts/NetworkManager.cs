using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

    public bool JoinRoom = true;

    void Awake () {
        //マスターサーバーへ接続
        PhotonNetwork.ConnectUsingSettings("v0.1");
    }
 
    void Update () {
    }
  
    //ロビー参加成功時のコールバック
    void OnJoinedLobby() {
        //ランダムにルームへ参加
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        foreach(RoomInfo info in roomList)
        {
            Debug.Log("player count of " + info.name + ": " + info.playerCount.ToString());
        }
        if (this.JoinRoom)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
 
    //ルーム参加失敗時のコールバック
    void OnPhotonRandomJoinFailed() {
        Debug.Log("ルームへの参加に失敗しました");
        PhotonNetwork.CreateRoom("room1");
    }
 
    //ルーム参加成功時のコールバック
    void OnJoinedRoom() {
        Debug.Log("ルームへの参加に成功しました");
    }
 
    void OnGUI() {
        //サーバーとの接続状態をGUIへ表示
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        foreach(RoomInfo info in roomList)
        {
            GUILayout.Label("player count of " + info.name + ": " + info.playerCount.ToString());
        }
    }
}
