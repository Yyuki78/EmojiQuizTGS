using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourではなくMonoBehaviourPunCallbacksを継承して、Photonのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }

    // マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
        Debug.Log("JoinRoom");
        GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
        //var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("GamePlayer", v, Quaternion.identity);
    }

    public void JorCRoom1()
    {
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }
    public void JorCRoom2()
    {
        PhotonNetwork.JoinOrCreateRoom("room2", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }
    public void JorCRoom3()
    {
        PhotonNetwork.JoinOrCreateRoom("room3", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }
    public void JorCRoom4()
    {
        PhotonNetwork.JoinOrCreateRoom("room4", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }
    public void LeaveInRoom()
    {
        PhotonNetwork.LeaveRoom();
        GameManager.Instance.SetCurrentState(GameManager.GameMode.RoomSelect);
    }
}