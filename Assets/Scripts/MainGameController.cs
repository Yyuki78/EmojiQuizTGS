using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainGameController : MonoBehaviourPunCallbacks
{
    public enum MainGameMode
    {
        InformRole,
        QuestionTime,
        ReportAnswer,
    }

    public static MainGameMode mainmode;//現在のモード

    private Player[] Players;//参加者のPlayer情報

    public int ParticipantsNum = 0;//参加人数

    public int QuesitionNum = 1;//問題数

    private bool Questioner = false;
    private bool Answerer = false;

    [SerializeField] GameObject QuestionerText;
    [SerializeField] GameObject AnswererText;

    private ThemaGenerator _themaGenerator;

    [SerializeField] GameObject QuestionerPanel;
    [SerializeField] GameObject AnswererPanel;

    [SerializeField] GameObject ReportAnswerPanel;

    [SerializeField] Image myIconImage;//左下に表示され続ける自分のアイコン


    private bool once = true;
    private bool isSendQuestion = false;

    private PhotonView _view;

    private InformRoleDisplay _inform;

    private ReportAnswer _answer;

    //音系
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Awake()
    {
        QuesitionNum = 1;

        _view = GetComponent<PhotonView>();

        _themaGenerator = GetComponent<ThemaGenerator>();

        _inform = GetComponent<InformRoleDisplay>();

        _answer = GetComponent<ReportAnswer>();

        _audio = AudioManager.GetComponent<AudioManager>();

        //全プレイヤー取得
        Players = PhotonNetwork.PlayerList;

        ParticipantsNum = Players.Length;

        Debug.Log("役割通知スタート！");
        OnGameStateChanged(MainGameMode.InformRole);
        /*
        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartInformRole), RpcTarget.All);
        }*/

        myIconImage.sprite = Resources.Load<Sprite>("IconImage/" + PhotonNetwork.LocalPlayer.GetScore());
    }

    // Update is called once per frame
    void Update()
    {
        //MainGameでないならReturn
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        if (isSendQuestion)
        {
            _view.RPC(nameof(RpcSendMessage), RpcTarget.All, _themaGenerator._themaNum, _themaGenerator._choicesNum);
            isSendQuestion = false;
        }

    }

    // 状態が変わったら何をするか
    void OnGameStateChanged(MainGameMode state)
    {
        //MainGameでないならReturn
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        mainmode = state;
        switch (state)
        {
            case MainGameMode.InformRole:
                StartCoroutine(InformRole());
                break;
            case MainGameMode.QuestionTime:
                StartCoroutine(QuestionTime());
                break;
            case MainGameMode.ReportAnswer:
                StartCoroutine(ReportAnswer());
                break;
            default:
                break;
        }
    }

    //役割通知　マスターからのRPCで実行
    private IEnumerator InformRole()
    {
        Debug.Log("役割通知");

        yield return new WaitForSeconds(0.01f);
        //変数の初期化
        Questioner = false;
        Answerer = false;
        QuestionerText.SetActive(false);
        AnswererText.SetActive(false);
        QuestionerPanel.SetActive(false);
        AnswererPanel.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        //選択した数字をリセット
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);
        ReportAnswerPanel.SetActive(false);

        yield return new WaitForSeconds(0.01f);

        //出題者・解答者を問題数から決定
        if (PhotonNetwork.LocalPlayer == Players[(QuesitionNum - 1) % ParticipantsNum])
        {
            //出題者
            Questioner = true;
            _audio.SE2();
        }
        else
        {
            //解答者
            Answerer = true;
            _audio.SE3();
        }
        yield return new WaitForSeconds(0.1f);

        //役割に応じた画面表示(アイコンの位置の設定)
        if (Questioner)
        {
            QuestionerText.SetActive(true);
        }
        if (Answerer)
        {
            AnswererText.SetActive(true);
        }
        StartCoroutine(_inform.showIcon(Players[(QuesitionNum - 1) % ParticipantsNum]));
        yield return new WaitForSeconds(0.1f);

        //マスターは問題の設定
        if (PhotonNetwork.IsMasterClient)
        {
            _themaGenerator.ThemaGenerate();
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(_themaGenerator.ChoicesGenerate());
        }
        
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.5f);
        if (QuesitionNum == 1)
        {
            yield return new WaitForSeconds(1f);
        }

        //カウントダウン表示
        yield return new WaitForSeconds(1f);

        if (once)
        {
            once = false;
            _answer.SetPosition(ParticipantsNum);
        }

        yield return new WaitForSeconds(1f);

        //マスターは問題を配布
        if (PhotonNetwork.IsMasterClient)
        {
            isSendQuestion = true;
        }

        yield return new WaitForSeconds(1f);

        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartQuestionTime), RpcTarget.All);
        }
    }

    //ゲーム中　マスターからのRPCで実行
    private IEnumerator QuestionTime()
    {
        yield return new WaitForSeconds(0.01f);
        //選択肢表示
        if (Questioner)
        {
            QuestionerPanel.SetActive(true);
        }
        if (Answerer)
        {
            AnswererPanel.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);

        //カウントダウン表示
        _audio.BGM3();

        yield return new WaitForSeconds(14.5f);
        //yield return new WaitForSeconds(3f);

        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartReportAnswer), RpcTarget.All);
        }
    }

    //リザルト　マスターからのRPCで実行
    private IEnumerator ReportAnswer()
    {
        ReportAnswerPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        //プレイヤーの解答表示
        //答えの表示
        _answer.ShareAnswer(QuesitionNum, Players[(QuesitionNum - 1) % ParticipantsNum]);

        yield return new WaitForSeconds(5.5f);
        //問題数の増加
        QuesitionNum++;
        yield return new WaitForSeconds(2.5f);

        //マスターは問題数が6ならリザルトへ行かせる
        if (PhotonNetwork.IsMasterClient && QuesitionNum == 6)
        {
            yield return new WaitForSeconds(1.0f);
            _view.RPC(nameof(StartResult), RpcTarget.All);
        }
        yield return new WaitForSeconds(0.1f);

        //次の問題へ
        if (PhotonNetwork.IsMasterClient)
        {
            if (QuesitionNum == 6) yield break;
            _view.RPC(nameof(StartInformRole), RpcTarget.All);
        }
    }

    //マスターによって実行される　ステートの変化
    [PunRPC]
    private void StartQuestionTime()
    {
        Debug.Log("真似・解答時間スタート！");
        OnGameStateChanged(MainGameMode.QuestionTime);
    }

    [PunRPC]
    private void StartReportAnswer()
    {
        Debug.Log("答え合わせスタート！");
        OnGameStateChanged(MainGameMode.ReportAnswer);
    }

    [PunRPC]
    private void StartInformRole()
    {
        Debug.Log("役割通知スタート！");
        OnGameStateChanged(MainGameMode.InformRole);
    }

    //お題・選択肢の配布
    [PunRPC]
    private void RpcSendMessage(int thema, int[] choices)
    {
        Debug.Log("お題と選択肢を受け取ってそれぞれ表示！");
        _themaGenerator.Showchoices(thema, choices);
    }

    private void distributionChoice(int thema, int[] choices)
    {
        StartCoroutine(Showchoice(thema, choices));
    }

    private IEnumerator Showchoice(int thema, int[] choices)
    {
        Debug.Log("お題と選択肢を受け取ってそれぞれ表示！");
        Debug.Log(thema + "," + choices[0] + "," + choices[1] + "," + choices[2] + "," + choices[3] + "," + choices[4]);
        yield return new WaitForSeconds(0.1f);
        _themaGenerator.Showchoices(thema, choices);
        yield break;
    }

    //リザルトへ移行
    [PunRPC]
    private void StartResult()
    {
        Debug.Log("ゲーム終了！！！");
        DebugGameManager.Instance.GoResult();
    }
}
