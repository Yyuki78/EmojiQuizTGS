using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MatchingSystem : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // ランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を5人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        //var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
        Debug.Log("接続されました");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }
}