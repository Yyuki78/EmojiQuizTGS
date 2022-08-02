using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager2 : MonoBehaviourPunCallbacks
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

    //�𓚎҂��o��҂��̒ʒm�p
    [SerializeField]
    GameObject QuestionerText;
    [SerializeField]
    GameObject AnswerersText;
    //�o��ҁE�𓚎҂̃p�l��
    [SerializeField]
    GameObject QuestionerPanel;
    [SerializeField]
    GameObject AnswerersPanel;
    //�o��ҁE�𓚎҂ŋ��ʂ��Ă��镔���̃p�l��
    [SerializeField]
    GameObject QuestionTimePanel;
    //�o��ҁE�𓚎҂̃p�l���ɂ���4�̃A�C�R���̕\��
    ShowIcon2 _showIcon2;
    //���𔭕\�p�̃p�l��
    [SerializeField]
    GameObject CorrectAnswerersPanel;
    //��ʐ؂�ւ����̉��o
    [SerializeField]
    GameObject ChangeImage;
    ChangeImage _changeImage;
    //�^�C�}�[�̕\��
    [SerializeField]
    GameObject Timer;
    //�Q�[���J�n���ɏ���������X�N���v�g
    ResetGame _reset;
    //BGM�ESE�̍Đ��p
    [SerializeField] GameObject SoundManager;
    SoundManager _sound;
    //�X�^���v�@�\�̕\���E��\��
    [SerializeField] GameObject StampBackImage;
    //�X�^���v�̏�̔������p�l��
    [SerializeField] GameObject ObstacleImage;

    //�}�X�^�[�̔ԍ�
    public static int MasterNum;
    //�T�[�o�[���������p
    private int TimeStamp;


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

        _changeImage = ChangeImage.GetComponent<ChangeImage>();
        _reset = GetComponent<ResetGame>();
        _showIcon2 = GetComponent<ShowIcon2>();
        _sound = SoundManager.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.MainGame)
        {
            if (preSetting)
            {
                StartCoroutine("Load");
                preSetting = false;
            }
        }/*
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.SetCurrentState(GameManager.GameMode.MainGame);
        }
        /*
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
                    if (sendall && modetime <= 2)
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
        }*/
    }
    private IEnumerator Load()
    {
        //���T�ڂ�
        playcount++;
        

        Debug.Log(playcount + "���[�h��ʁ{�𓚎ҏo��҂̔��\�����܂�");
        LoadingBoard.SetActive(true);
        mainmode = MainGameMode.PlayerSelect;
        yield return new WaitForSeconds(0.2f);

        //�T�[�o�[���Ԃ̓���
        if (playcount > 1)
        {
            while (PhotonNetwork.ServerTimestamp - TimeStamp > 35000)
            {
                // 1�t���[��������҂��܂��B
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.5f);
        TimeStamp = PhotonNetwork.ServerTimestamp;


        //�������}�X�^�[���ǂ���
        //���Ԃ��Ȃ��̂ŁA��荇�����}�X�^�[��10��o�肷��`�Ŏ���
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            _sound.SE2();
            MasterNum = MatchmakingView.PlayerNum;
            questioner = true;
        }
        else
        {
            _sound.SE3();
            questioner = false;
        }
        yield return new WaitForSeconds(0.5f);
        if (questioner)
        {
            QuestionerText.SetActive(true);
        }
        else
        {
            AnswerersText.SetActive(true);
        }
        yield return new WaitForSeconds(2.5f);
        if (playcount == 1)
        {
            yield return new WaitForSeconds(0.5f);
        }

        _changeImage.Init();
        // �w��b�ԑ҂�
        yield return new WaitForSeconds(0.67f);
        LoadingBoard.SetActive(false);
        StartCoroutine("StartGame");
        yield break;
    }

    private IEnumerator StartGame()
    {
        Debug.Log(playcount + "�Q�[�����n�߂܂�");
        mainmode = MainGameMode.QuestionTime;
        _sound.BGM2();
        //yield return new WaitForSeconds(1.0f);
        if (questioner)
        {
            QuestionerPanel.SetActive(true);
            AnswerersPanel.SetActive(false);
            ObstacleImage.SetActive(true);
        }
        else
        {
            QuestionerPanel.SetActive(false);
            AnswerersPanel.SetActive(true);
            ObstacleImage.SetActive(false);
        }
        StampBackImage.SetActive(true);
        QuestionTimePanel.SetActive(true);
        StartCoroutine(_showIcon2.showIcon());
        Timer.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        //�����Ŏ��Ԃ̊Ǘ�
        //�����ŃT�[�o�[���ԓ�����������


        yield return new WaitForSeconds(15.5f);//�Q�[����

        _changeImage.Init();
        // �w��b�ԑ҂�
        yield return new WaitForSeconds(0.7f);

        //�������琳�𔭕\
        QuestionerPanel.SetActive(false);
        AnswerersPanel.SetActive(false);
        QuestionTimePanel.SetActive(false);

        StartCoroutine("CorrectAnswer");
        
        yield break;
    }

    private IEnumerator CorrectAnswer()
    {
        mainmode = MainGameMode.ShareAnswer;
        Debug.Log(playcount + "���𔭕\�Ɉڂ�܂�");
        CorrectAnswerersPanel.SetActive(true);

        yield return new WaitForSeconds(12.0f);//�����𔭕\��
        if (playcount == 10)
        {
            ObstacleImage.SetActive(false);
            GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
            yield break;
        }

        ObstacleImage.SetActive(true);
        _changeImage.Init();
        yield return new WaitForSeconds(0.1f);

        //�������玟�̎���̏���
        mainmode = MainGameMode.ReportQuestion;
        //�o��҂�؂�ւ���
        var players = PhotonNetwork.PlayerList;
        /*foreach (var player in players)
        {
            Debug.Log("�v���C���[�̔ԍ�����" + MatchmakingView.PlayerNum2);
            if (MatchmakingView.PlayerNum2 == ((playcount % 5) + 1))
            {
                Debug.Log("�o��҂ɂȂ�܂���");
                PhotonNetwork.SetMasterClient(player);
            }
        }*/
        if (MatchmakingView.PlayerNum2 == ((playcount % 5) + 1))
        {
            Debug.Log("�o��҂ɂȂ�܂���");
            PhotonNetwork.SetMasterClient(players[MatchmakingView.PlayerNum2 - 1]);
        }
        if (playcount == 6)
        {
            PhotonNetwork.SetMasterClient(players[1]);
        }

        yield return new WaitForSeconds(0.6f);
        StampBackImage.SetActive(false);
        _reset.ResetAll();
        yield return new WaitForSeconds(0.2f);
        preSetting = true;
        yield break;
    }


    //�Q�[���I�����ɏ���������
    public void Init()
    {
        LoadingBoard.SetActive(false);
        AnswerBoard.SetActive(false);
        ChoiceBoard.SetActive(false);

        QuestionerText.SetActive(false);
        AnswerersText.SetActive(false);
        QuestionerPanel.SetActive(false);
        AnswerersPanel.SetActive(false);
        QuestionTimePanel.SetActive(false);
        CorrectAnswerersPanel.SetActive(false);
        //Timer.SetActive(false);
        StampBackImage.SetActive(false);

        Debug.Log((playcount + 1) + "�T�ڂ̃Q�[�����J�n���܂�");
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
        M_photonView.RPC(nameof(M_operate.SelectPlayer), RpcTarget.MasterClient, playerOrder[playcount % playernumber]);
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
        for (int i = 0; i < players; i++)
        {
            playerOrder[i] = (byte)i;
        }
        for (int i = 0; i < players; i++)
        {
            byte tmp = playerOrder[i];
            byte rand = (byte)Random.Range(0, players);
            playerOrder[i] = playerOrder[rand];
            playerOrder[rand] = tmp;
        }
    }

}