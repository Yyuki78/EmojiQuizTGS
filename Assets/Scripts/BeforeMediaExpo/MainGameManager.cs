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

    public static MainGameMode mainmode;//現在のモード
    private static MainGameMode premainmode;//変更先のモード
    public static int playcount = 0;//何週目の表示かを教える
    private bool preSetting;//モード切替後最初の更新かどうかをとる
    public static float modetime;//後でprivateに変える//モードを切り替えるまでの時間をとる
    private int SST;//SendServerTimeで受け取ったServerTimeを保持
    private byte[] playerOrder;//出題者の順番を保持
    private bool sendall;//データを全体に送信したかを受け取る
    private int playernumber;//ルーム内のプレイヤーの人数を取る
    private byte myAnswer;//自身の答えを保持
    private byte[] ourAnswer;//皆の答えを保持
    private bool questioner;//出題者か否かを保持

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
        //スタートの時間を受け取り
        SST = PhotonNetwork.ServerTimestamp;
        M_photonView.RPC(nameof(M_operate.SendServerTime), RpcTarget.MasterClient, SST);
        SST = M_operate.getStandbyTime();
        
        //出題者の順番を決定
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
        //自分のプレイヤー番号と比較して出題者を取得
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
        //M_photonView.RPC(nameof(M_operate.OperateQuestion), RpcTarget.MasterClient, 絵文字の選択肢番号のbyte配列, 答えの番号);
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
