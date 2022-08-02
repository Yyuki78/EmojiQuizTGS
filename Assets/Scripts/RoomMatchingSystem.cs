using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomMatchingSystem : MonoBehaviourPunCallbacks
{
    private RoomList roomList = new RoomList();
    private List<RoomButton> roomButtonList = new List<RoomButton>();
    private CanvasGroup canvasGroup;

    //Playerの番号
    public static int PlayerNum = 0;
    public static int PlayerNum2 = 0;
    private int PlayerCount = 0;

    IconDisplay _iconDisplay;

    [SerializeField]
    Text joinedMembersText;

    [SerializeField]
    GameObject ReadyButton;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _iconDisplay = GetComponent<IconDisplay>();
        // ロビーに参加するまでは、全てのルーム参加ボタンを押せないようにする
        canvasGroup.interactable = false;

        // 全てのルーム参加ボタンを初期化する
        int roomId = 1;
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<RoomButton>(out var roomButton))
            {
                roomButton.Init(this, roomId++);
                roomButtonList.Add(roomButton);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        // ロビーに参加したら、ルーム参加ボタンを押せるようにする
        canvasGroup.interactable = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> changedRoomList)
    {
        roomList.Update(changedRoomList);

        // 全てのルーム参加ボタンの表示を更新する
        foreach (var roomButton in roomButtonList)
        {
            if (roomList.TryGetRoomInfo(roomButton.RoomName, out var roomInfo))
            {
                roomButton.SetPlayerCount(roomInfo.PlayerCount);
            }
            else
            {
                roomButton.SetPlayerCount(0);
            }
        }
    }

    public void OnJoiningRoom()
    {
        // ルーム参加処理中は、全てのルーム参加ボタンを押せないようにする
        canvasGroup.interactable = false;
    }

    //Roomに入った
    public override void OnJoinedRoom()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
        Debug.Log(PhotonNetwork.NickName + " is joined.");

        //自分の番号を取得する
        PlayerNum2 = PhotonNetwork.PlayerList.Length;

        UpdateMemberList();
        _iconDisplay.UpdateLabel();
        // ルームへの参加が成功したら、UIを非表示にする
        //gameObject.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.NickName + " is joined.");
        UpdateMemberList();
        _iconDisplay.UpdateLabel();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ルームへの参加が失敗したら、再びルーム参加ボタンを押せるようにする
        canvasGroup.interactable = true;
    }

    public void UpdateMemberList()
    {
        PlayerCount = 0;
        joinedMembersText.text = "";
        foreach (var p in PhotonNetwork.PlayerList)
        {
            PlayerCount++;
            joinedMembersText.text += PhotonNetwork.NickName + "\n";
        }
        if (PlayerCount == 5)
        {
            Debug.Log("5人が揃いました");
            ReadyButton.SetActive(true);
        }
    }
}
