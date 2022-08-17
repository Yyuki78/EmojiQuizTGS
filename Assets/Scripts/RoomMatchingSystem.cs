using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomMatchingSystem : MonoBehaviourPunCallbacks
{
    // �ő�l��
    [SerializeField] private int maxPlayers = 5;

    // ���J�E����J
    [SerializeField] private bool isVisible = true;

    // ������
    [SerializeField] private string roomName1 = "Room1";
    [SerializeField] private string roomName2 = "Room2";

    private PhotonView _view;

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;


    /////////////////////////////////////////////////////////////////////////////////////
    // Awake & Start ////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    // Awake
    private void Awake()
    {
        // �V�[���̎�������: ����
        PhotonNetwork.AutomaticallySyncScene = false;
    }


    // Start is called before the first frame update
    private void Start()
    {
        // Photon�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();
        _view = GetComponent<PhotonView>();
        _audio = AudioManager.GetComponent<AudioManager>();
    }
    /*
    // �j�b�N�l�[����t����
    private void SetMyNickName(string nickName)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
    }*/

    // ���r�[�ɓ���
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //�Ƃ̃{�^��(����)
    //�����ɓ������� �i���݂��Ȃ���΍쐬���ē�������j
    public void JoinOrCreateRoom1()
    {
        Debug.Log("�����ɓ��肽���@1");
        // ���[���I�v�V�����̊�{�ݒ�
        RoomOptions roomOptions = new RoomOptions
        {
            // �����̍ő�l��
            MaxPlayers = (byte)maxPlayers,

            // ���J
            IsVisible = isVisible,
        };

        // ���� (���݂��Ȃ���Ε������쐬���ē�������)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName1, roomOptions, TypedLobby.Default);
        }
    }

    //�Ƃ̃{�^��(�E��)
    //�����ɓ������� �i���݂��Ȃ���΍쐬���ē�������j
    public void JoinOrCreateRoom2()
    {
        Debug.Log("�����ɓ��肽���@2");
        // ���[���I�v�V�����̊�{�ݒ�
        RoomOptions roomOptions = new RoomOptions
        {
            // �����̍ő�l��
            MaxPlayers = (byte)maxPlayers,

            // ���J
            IsVisible = isVisible,
        };

        // ���� (���݂��Ȃ���Ε������쐬���ē�������)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName2, roomOptions, TypedLobby.Default);
        }
    }

    // ��������ގ�����
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            // �ގ�
            PhotonNetwork.LeaveRoom();
        }
    }

    // Photon�ɐڑ�������
    public override void OnConnected()
    {
        Debug.Log("OnConnected");

        // �j�b�N�l�[����t����
        //SetMyNickName("Knohhoso");
    }


    // Photon����ؒf���ꂽ��
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }


    // �}�X�^�[�T�[�o�[�ɐڑ�������
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        // ���r�[�ɓ���
        JoinLobby();
    }


    // ���r�[�ɓ�������
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }


    // ���r�[����o����
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }


    // �������쐬������
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }


    // �����̍쐬�Ɏ��s������
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }


    // �����ɓ���������
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        //�v���[���[�̃J�X�^���v���p�e�B�X�V
        SetMyCustomProperties();

        //�X�e�[�g��InRoom�ɕύX
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.InRoom);


        /*
        // �����̏���\��
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("RoomName: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("HostName: " + PhotonNetwork.MasterClient.NickName);
            Debug.Log("Stage: " + PhotonNetwork.CurrentRoom.CustomProperties["Stage"] as string);
            Debug.Log("Difficulty: " + PhotonNetwork.CurrentRoom.CustomProperties["Difficulty"] as string);
            Debug.Log("Slots: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }*/
    }


    // ����̕����ւ̓����Ɏ��s������
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _audio.SE7();
        Debug.Log("OnJoinRoomFailed");

        PhotonNetwork.JoinLobby();
    }


    // �����_���ȕ����ւ̓����Ɏ��s������
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
    }


    // ��������ގ�������
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }


    // ���̃v���C���[���������Ă�����
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
    }


    // �}�X�^�[�N���C�A���g���ς������
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched");
    }


    // ���r�[�ɍX�V����������
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }


    // ���[�����X�g�ɍX�V����������
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }


    // ���[���v���p�e�B���X�V���ꂽ��
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }


    // �v���C���[�v���p�e�B���X�V���ꂽ��
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    /// <summary>
    /// �v���C���[�ɔԍ���^����
    /// </summary>
    private void SetMyCustomProperties()
    {
        List<int> playerSetableCountList = new List<int>();

        //�����l���܂ł̐����̃��X�g���쐬
        //��) �����l�� = 4 �̏ꍇ�A{0,1,2,3}
        int count = 0;
        for (int i = 0; i < maxPlayers; i++)
        {
            playerSetableCountList.Add(count);
            count++;
        }

        //���̑S�v���C���[�擾
        Player[] otherPlayers = PhotonNetwork.PlayerListOthers;

        //���̃v���C���[�����Ȃ���΃J�X�^���v���p�e�B�̒l��"0"�ɐݒ�
        if (otherPlayers.Length <= 0)
        {
            //���[�J���̃v���C���[�̃J�X�^���v���p�e�B��ݒ�
            int playerAssignNum = otherPlayers.Length;
            PhotonNetwork.LocalPlayer.UpdatePlayerNum(playerAssignNum);
            return;
        }

        //���̃v���C���[�̃J�X�^���v���p�e�B�[�擾���ă��X�g�쐬
        List<int> playerAssignNums = new List<int>();
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            playerAssignNums.Add(otherPlayers[i].GetPlayerNum());
        }

        //���X�g���m���r���A���g�p�̐����̃��X�g���쐬
        //��) 0,1�Ƀv���[���[�����݂���ꍇ�A�Ԃ����X�g��2,3
        playerSetableCountList.RemoveAll(playerAssignNums.Contains);

        //���[�J���̃v���C���[�̃J�X�^���v���p�e�B��ݒ�
        //�󂢂Ă���ꏊ�̂����A��ԎႢ�����̉ӏ��𗘗p
        PhotonNetwork.LocalPlayer.UpdatePlayerNum(playerSetableCountList[0]);

        Debug.Log("PlayerNum:" + playerSetableCountList[0]);
    }
}
