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

    //Player�̔ԍ�
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
        // ���r�[�ɎQ������܂ł́A�S�Ẵ��[���Q���{�^���������Ȃ��悤�ɂ���
        canvasGroup.interactable = false;

        // �S�Ẵ��[���Q���{�^��������������
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
        // ���r�[�ɎQ��������A���[���Q���{�^����������悤�ɂ���
        canvasGroup.interactable = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> changedRoomList)
    {
        roomList.Update(changedRoomList);

        // �S�Ẵ��[���Q���{�^���̕\�����X�V����
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
        // ���[���Q���������́A�S�Ẵ��[���Q���{�^���������Ȃ��悤�ɂ���
        canvasGroup.interactable = false;
    }

    //Room�ɓ�����
    public override void OnJoinedRoom()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
        Debug.Log(PhotonNetwork.NickName + " is joined.");

        //�����̔ԍ����擾����
        PlayerNum2 = PhotonNetwork.PlayerList.Length;

        UpdateMemberList();
        _iconDisplay.UpdateLabel();
        // ���[���ւ̎Q��������������AUI���\���ɂ���
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
        // ���[���ւ̎Q�������s������A�Ăу��[���Q���{�^����������悤�ɂ���
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
            Debug.Log("5�l�������܂���");
            ReadyButton.SetActive(true);
        }
    }
}
