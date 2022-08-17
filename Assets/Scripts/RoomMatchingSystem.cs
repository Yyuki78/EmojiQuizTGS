using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomMatchingSystem : MonoBehaviourPunCallbacks
{
    // 最大人数
    [SerializeField] private int maxPlayers = 5;

    // 公開・非公開
    [SerializeField] private bool isVisible = true;

    // 部屋名
    [SerializeField] private string roomName1 = "Room1";
    [SerializeField] private string roomName2 = "Room2";

    private PhotonView _view;

    //音系
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;


    /////////////////////////////////////////////////////////////////////////////////////
    // Awake & Start ////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    // Awake
    private void Awake()
    {
        // シーンの自動同期: 無効
        PhotonNetwork.AutomaticallySyncScene = false;
    }


    // Start is called before the first frame update
    private void Start()
    {
        // Photonに接続
        PhotonNetwork.ConnectUsingSettings();
        _view = GetComponent<PhotonView>();
        _audio = AudioManager.GetComponent<AudioManager>();
    }
    /*
    // ニックネームを付ける
    private void SetMyNickName(string nickName)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
    }*/

    // ロビーに入る
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //家のボタン(左側)
    //部屋に入室する （存在しなければ作成して入室する）
    public void JoinOrCreateRoom1()
    {
        Debug.Log("部屋に入りたい　1");
        // ルームオプションの基本設定
        RoomOptions roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = (byte)maxPlayers,

            // 公開
            IsVisible = isVisible,
        };

        // 入室 (存在しなければ部屋を作成して入室する)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName1, roomOptions, TypedLobby.Default);
        }
    }

    //家のボタン(右側)
    //部屋に入室する （存在しなければ作成して入室する）
    public void JoinOrCreateRoom2()
    {
        Debug.Log("部屋に入りたい　2");
        // ルームオプションの基本設定
        RoomOptions roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = (byte)maxPlayers,

            // 公開
            IsVisible = isVisible,
        };

        // 入室 (存在しなければ部屋を作成して入室する)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName2, roomOptions, TypedLobby.Default);
        }
    }

    // 部屋から退室する
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            // 退室
            PhotonNetwork.LeaveRoom();
        }
    }

    // Photonに接続した時
    public override void OnConnected()
    {
        Debug.Log("OnConnected");

        // ニックネームを付ける
        //SetMyNickName("Knohhoso");
    }


    // Photonから切断された時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }


    // マスターサーバーに接続した時
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        // ロビーに入る
        JoinLobby();
    }


    // ロビーに入った時
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }


    // ロビーから出た時
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }


    // 部屋を作成した時
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }


    // 部屋の作成に失敗した時
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }


    // 部屋に入室した時
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        //プレーヤーのカスタムプロパティ更新
        SetMyCustomProperties();

        //ステートをInRoomに変更
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.InRoom);


        /*
        // 部屋の情報を表示
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("RoomName: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("HostName: " + PhotonNetwork.MasterClient.NickName);
            Debug.Log("Stage: " + PhotonNetwork.CurrentRoom.CustomProperties["Stage"] as string);
            Debug.Log("Difficulty: " + PhotonNetwork.CurrentRoom.CustomProperties["Difficulty"] as string);
            Debug.Log("Slots: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }*/
    }


    // 特定の部屋への入室に失敗した時
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _audio.SE7();
        Debug.Log("OnJoinRoomFailed");

        PhotonNetwork.JoinLobby();
    }


    // ランダムな部屋への入室に失敗した時
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
    }


    // 部屋から退室した時
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }


    // 他のプレイヤーが入室してきた時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
    }


    // マスタークライアントが変わった時
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched");
    }


    // ロビーに更新があった時
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }


    // ルームリストに更新があった時
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }


    // ルームプロパティが更新された時
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }


    // プレイヤープロパティが更新された時
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    /// <summary>
    /// プレイヤーに番号を与える
    /// </summary>
    private void SetMyCustomProperties()
    {
        List<int> playerSetableCountList = new List<int>();

        //制限人数までの数字のリストを作成
        //例) 制限人数 = 4 の場合、{0,1,2,3}
        int count = 0;
        for (int i = 0; i < maxPlayers; i++)
        {
            playerSetableCountList.Add(count);
            count++;
        }

        //他の全プレイヤー取得
        Player[] otherPlayers = PhotonNetwork.PlayerListOthers;

        //他のプレイヤーがいなければカスタムプロパティの値を"0"に設定
        if (otherPlayers.Length <= 0)
        {
            //ローカルのプレイヤーのカスタムプロパティを設定
            int playerAssignNum = otherPlayers.Length;
            PhotonNetwork.LocalPlayer.UpdatePlayerNum(playerAssignNum);
            return;
        }

        //他のプレイヤーのカスタムプロパティー取得してリスト作成
        List<int> playerAssignNums = new List<int>();
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            playerAssignNums.Add(otherPlayers[i].GetPlayerNum());
        }

        //リスト同士を比較し、未使用の数字のリストを作成
        //例) 0,1にプレーヤーが存在する場合、返すリストは2,3
        playerSetableCountList.RemoveAll(playerAssignNums.Contains);

        //ローカルのプレイヤーのカスタムプロパティを設定
        //空いている場所のうち、一番若い数字の箇所を利用
        PhotonNetwork.LocalPlayer.UpdatePlayerNum(playerSetableCountList[0]);

        Debug.Log("PlayerNum:" + playerSetableCountList[0]);
    }
}
