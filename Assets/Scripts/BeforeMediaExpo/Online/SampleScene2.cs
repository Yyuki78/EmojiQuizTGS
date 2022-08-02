using Photon.Pun;
using UnityEngine;

public class SampleScene2 : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続しました");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーに参加しました");
    }

    public override void OnJoinedRoom()
    {
        //var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
        Debug.Log("部屋に参加しました。");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }
}