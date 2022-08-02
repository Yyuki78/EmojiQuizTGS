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

    //解答者か出題者かの通知用
    [SerializeField]
    GameObject QuestionerText;
    [SerializeField]
    GameObject AnswerersText;
    //出題者・解答者のパネル
    [SerializeField]
    GameObject QuestionerPanel;
    [SerializeField]
    GameObject AnswerersPanel;
    //出題者・解答者で共通している部分のパネル
    [SerializeField]
    GameObject QuestionTimePanel;
    //出題者・解答者のパネルにある4つのアイコンの表示
    ShowIcon2 _showIcon2;
    //正解発表用のパネル
    [SerializeField]
    GameObject CorrectAnswerersPanel;
    //画面切り替え時の演出
    [SerializeField]
    GameObject ChangeImage;
    ChangeImage _changeImage;
    //タイマーの表示
    [SerializeField]
    GameObject Timer;
    //ゲーム開始時に初期化するスクリプト
    ResetGame _reset;
    //BGM・SEの再生用
    [SerializeField] GameObject SoundManager;
    SoundManager _sound;
    //スタンプ機能の表示・非表示
    [SerializeField] GameObject StampBackImage;
    //スタンプの上の半透明パネル
    [SerializeField] GameObject ObstacleImage;

    //マスターの番号
    public static int MasterNum;
    //サーバー時刻同期用
    private int TimeStamp;


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
        //何週目か
        playcount++;
        

        Debug.Log(playcount + "ロード画面＋解答者出題者の発表をします");
        LoadingBoard.SetActive(true);
        mainmode = MainGameMode.PlayerSelect;
        yield return new WaitForSeconds(0.2f);

        //サーバー時間の同期
        if (playcount > 1)
        {
            while (PhotonNetwork.ServerTimestamp - TimeStamp > 35000)
            {
                // 1フレーム処理を待ちます。
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.5f);
        TimeStamp = PhotonNetwork.ServerTimestamp;


        //自分がマスターかどうか
        //時間がないので、取り合えずマスターが10問出題する形で実装
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
        // 指定秒間待つ
        yield return new WaitForSeconds(0.67f);
        LoadingBoard.SetActive(false);
        StartCoroutine("StartGame");
        yield break;
    }

    private IEnumerator StartGame()
    {
        Debug.Log(playcount + "ゲームを始めます");
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
        //ここで時間の管理
        //ここでサーバー時間同期をしたい


        yield return new WaitForSeconds(15.5f);//ゲーム中

        _changeImage.Init();
        // 指定秒間待つ
        yield return new WaitForSeconds(0.7f);

        //ここから正解発表
        QuestionerPanel.SetActive(false);
        AnswerersPanel.SetActive(false);
        QuestionTimePanel.SetActive(false);

        StartCoroutine("CorrectAnswer");
        
        yield break;
    }

    private IEnumerator CorrectAnswer()
    {
        mainmode = MainGameMode.ShareAnswer;
        Debug.Log(playcount + "正解発表に移ります");
        CorrectAnswerersPanel.SetActive(true);

        yield return new WaitForSeconds(12.0f);//今正解発表中
        if (playcount == 10)
        {
            ObstacleImage.SetActive(false);
            GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
            yield break;
        }

        ObstacleImage.SetActive(true);
        _changeImage.Init();
        yield return new WaitForSeconds(0.1f);

        //ここから次の周回の準備
        mainmode = MainGameMode.ReportQuestion;
        //出題者を切り替える
        var players = PhotonNetwork.PlayerList;
        /*foreach (var player in players)
        {
            Debug.Log("プレイヤーの番号羅列" + MatchmakingView.PlayerNum2);
            if (MatchmakingView.PlayerNum2 == ((playcount % 5) + 1))
            {
                Debug.Log("出題者になりました");
                PhotonNetwork.SetMasterClient(player);
            }
        }*/
        if (MatchmakingView.PlayerNum2 == ((playcount % 5) + 1))
        {
            Debug.Log("出題者になりました");
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


    //ゲーム終了時に初期化する
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

        Debug.Log((playcount + 1) + "週目のゲームを開始します");
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
        M_photonView.RPC(nameof(M_operate.SelectPlayer), RpcTarget.MasterClient, playerOrder[playcount % playernumber]);
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