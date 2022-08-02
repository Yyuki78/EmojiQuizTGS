using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviour�ł͂Ȃ�MonoBehaviourPunCallbacks���p�����āAPhoton�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // PhotonServerSettings�ɐݒ肵�����e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }

    // �}�b�`���O�������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �}�b�`���O��A�����_���Ȉʒu�Ɏ������g�̃l�b�g���[�N�I�u�W�F�N�g�𐶐�����
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