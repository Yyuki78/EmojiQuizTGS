using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InRoom : MonoBehaviourPunCallbacks
{
    private PhotonView _view;

    private bool isStart = false;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            //�S�v���C���[�擾
            Player[] Players = PhotonNetwork.PlayerList;

            //���ɐl�����Ȃ��Ȃ�return
            if (Players.Length <= 1) return;

            //�����������ǂ�����l�������肵������
            for (int i = 0; i < Players.Length; i++)
            {
                bool hantei = Players[i].GetReady();
                if (!hantei)
                {
                    return;
                }
            }

            //�S�������������Ȃ�
            _view.RPC(nameof(StartMainGame), RpcTarget.All);
        }

        if (isStart && once)
        {
            StartCoroutine(CountDown());
            once = false;
        }
    }

    [PunRPC]
    private void StartMainGame()
    {
        isStart = true;
    }

    private IEnumerator CountDown()
    {
        //�J�E���g�_�E��
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("�Q�[���X�^�[�g�I");
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.MainGame);
        yield break;
    }


    public void OnClickIcon(int IconNum)
    {
        //���������{�^����������悤�ɂ���

        // �A�C�R����ݒ肷��

    }

    public void OnClickReadyButton()
    {
        Debug.Log("���������I");
        // ��������
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetReady(true);
        }
        // ���������{�^�����o��
    }
}
