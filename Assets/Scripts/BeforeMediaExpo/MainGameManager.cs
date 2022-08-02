using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MainGameManager : MonoBehaviourPunCallbacks
{
    public enum MainGameMode
    {
        LoadGame = 0,
        PlayerSelect,
        ReportQuestion,
        QuestionTime,
        ShareAnswer,
        ReportAnswer,
    }

    [SerializeField]
    GameObject LoadingBoard;
    [SerializeField]
    GameObject AnswerBoard;
    [SerializeField]
    GameObject ChoiceBoard;
    [SerializeField]
    GameObject[] PlayerMode;
    [SerializeField]
    GameObject[] AnswerersBoard;

    public static MainGameMode mainmode;//���݂̃��[�h
    private static MainGameMode premainmode;//�ύX��̃��[�h
    public static int playcount = 0;//���T�ڂ̕\������������
    private bool preSetting;//���[�h�ؑ֌�ŏ��̍X�V���ǂ������Ƃ�
    public static float modetime;//���private�ɕς���//���[�h��؂�ւ���܂ł̎��Ԃ��Ƃ�
    private int SST;//SendServerTime�Ŏ󂯎����ServerTime��ێ�
    private byte[] playerOrder;//�o��҂̏��Ԃ�ێ�
    private bool sendall;//�f�[�^��S�̂ɑ��M���������󂯎��
    private int playernumber;//���[�����̃v���C���[�̐l�������
    private byte myAnswer;//���g�̓�����ێ�
    private byte[] ourAnswer;//�F�̓�����ێ�
    private bool questioner;//�o��҂��ۂ���ێ�

    private PhotonView M_photonView;
    private NetworkOperate M_operate;
    // Start is called before the first frame update
    void Start()
    {
        preSetting = true;
        M_photonView = this.GetComponent<PhotonView>();
        M_operate = this.GetComponent<NetworkOperate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.MainGame)
        {
            modetime -= (float)Time.deltaTime;
            switch (mainmode)
            {
                case MainGameMode.LoadGame:
                    if (preSetting)
                    {
                        preLG();
                    }
                    if (PhotonNetwork.ServerTimestamp - SST >= 3000)
                    {
                        premainmode = MainGameMode.PlayerSelect;
                    }
                    break;
                case MainGameMode.PlayerSelect:
                    if (preSetting)
                    {
                        prePS();
                    }
                    if (modetime <= 0)
                    {
                        premainmode = MainGameMode.ReportQuestion;
                    }
                    break;
                case MainGameMode.ReportQuestion:
                    if (preSetting)
                    {
                        preRQ();
                    }
                    if (modetime <= 0)
                    {
                        premainmode = MainGameMode.QuestionTime;
                    }
                    break;
                case MainGameMode.QuestionTime:
                    if (preSetting)
                    {
                        preQT();
                    }
                    if (modetime <= 0)
                    {
                        premainmode = MainGameMode.ReportAnswer;
                    }
                    break;
                case MainGameMode.ReportAnswer:
                    if (preSetting)
                    {
                        preRA();
                    }
                    if(sendall && modetime <= 2)
                    {
                        sendall = false;
                        Debug.Log(ourAnswer);
                        ourAnswer = M_operate.getPlayerAnswer();
                        M_photonView.RPC(nameof(M_operate.SendOurAnswers), RpcTarget.All, ourAnswer);
                    }
                    if (modetime <= 0)
                    {
                        premainmode = MainGameMode.ShareAnswer;
                    }
                    break;
                case MainGameMode.ShareAnswer:
                    if (preSetting)
                    {
                        preSA();
                    }
                    if (PhotonNetwork.ServerTimestamp - SST >= 8000)
                    {
                        premainmode = MainGameMode.PlayerSelect;
                    }
                    break;
            }
            if (mainmode != premainmode)
            {
                switch (premainmode)
                {
                    case MainGameMode.PlayerSelect:
                        mainmode = MainGameMode.PlayerSelect;
                        break;
                    case MainGameMode.ReportQuestion:
                        mainmode = MainGameMode.ReportQuestion;
                        break;
                    case MainGameMode.QuestionTime:
                        mainmode = MainGameMode.QuestionTime;
                        break;
                    case MainGameMode.ShareAnswer:
                        mainmode = MainGameMode.ShareAnswer;
                        break;
                    case MainGameMode.ReportAnswer:
                        mainmode = MainGameMode.ReportAnswer;
                        break;
                }
                preSetting = true;
            }
        }
    }
    private void preLG()
    {
        playcount++;
        LoadingBoard.SetActive(true);
        //playernumber = PhotonNetwork.PlayerList.Length;
        playernumber = 4;
        //�X�^�[�g�̎��Ԃ��󂯎��
        SST = PhotonNetwork.ServerTimestamp;
        M_photonView.RPC(nameof(M_operate.SendServerTime), RpcTarget.MasterClient, SST);
        SST = M_operate.getStandbyTime();
        
        //�o��҂̏��Ԃ�����
        if (PhotonNetwork.IsMasterClient)
        {
            Shuffle(/*(byte)playernumber + 1*/ 5);
        }
        M_photonView.RPC(nameof(M_operate.SelectPlayer), RpcTarget.MasterClient, playerOrder[playcount%playernumber]);
        Debug.Log(playerOrder[playcount % playernumber]);
        preSetting = false;
    }
    private void prePS()
    {
        //�����̃v���C���[�ԍ��Ɣ�r���ďo��҂��擾
        //questioner = (PhotonNetwork.PlayerList.)
        questioner = Random.value > 0.5f;
        LoadingBoard.SetActive(false);
        switch (playernumber)
        {
            case 2: AnswerersBoard[0].SetActive(false); break;
            case 3: AnswerersBoard[1].SetActive(false); break;
            case 4: AnswerersBoard[2].SetActive(false); break;
            default: AnswerersBoard[2].SetActive(false); break;
        }
        if (questioner)
        {
            PlayerMode[0].SetActive(true);
        }
        else
        {
            PlayerMode[1].SetActive(true);
        }
        modetime = 3.0f;
        //M_photonView.RPC(nameof(M_operate.OperateQuestion), RpcTarget.MasterClient, �G�����̑I�����ԍ���byte�z��, �����̔ԍ�);
        preSetting = false; 
    }
    private void preRQ()
    {
        if (questioner)
        {
            PlayerMode[0].SetActive(false);
            AnswerBoard.SetActive(true);
        }
        else
        {
            PlayerMode[1].SetActive(false);
            ChoiceBoard.SetActive(true);
        }
        modetime = 3.0f;
        preSetting = false;
    }
    private void preQT()
    {
        modetime = 10.0f;
        preSetting = false;
    }
    private void preRA()
    {
        if (!questioner)
        {
            ChoiceBoard.SetActive(false);
            AnswerBoard.SetActive(true);
        }
        M_photonView.RPC(nameof(M_operate.SendMyAnswer), RpcTarget.All, myAnswer, (byte)playernumber);
        modetime = 4.0f;
        preSetting = false;
        sendall = true;
    }
    private void preSA()
    {
        playcount++;
        SST = PhotonNetwork.ServerTimestamp;
        M_photonView.RPC(nameof(M_operate.SendServerTime), RpcTarget.MasterClient, SST);
        SST = M_operate.getStandbyTime();
        M_photonView.RPC(nameof(M_operate.SelectPlayer), RpcTarget.MasterClient, playerOrder[playcount % playernumber]);
        AnswerBoard.SetActive(false);
        switch (playernumber)
        {
            case 2: AnswerersBoard[0].SetActive(true); break;
            case 3: AnswerersBoard[1].SetActive(true); break;
            case 4: AnswerersBoard[2].SetActive(true); break;
            default: AnswerersBoard[2].SetActive(true); break; 
        }
        preSetting = false;
    }

    private void Shuffle(byte players)
    {
        playerOrder = new byte[players];
        for(int i = 0; i < players; i++)
        {
            playerOrder[i] = (byte)i;
        }
        for(int i = 0; i < players; i++)
        {
            byte tmp = playerOrder[i];
            byte rand = (byte)Random.Range(0, players);
            playerOrder[i] = playerOrder[rand];
            playerOrder[rand] = tmp;
        }
    }

}
