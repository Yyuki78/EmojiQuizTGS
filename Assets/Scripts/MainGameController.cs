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

    [SerializeField] Image TransitionStateImage;//画面が切り替わる際の演出用画像
    [SerializeField] Sprite[] TransitionImage = new Sprite[3];
    [SerializeField] Image TransitionStateBGImage;
    [SerializeField] Sprite[] TransitionBGImage = new Sprite[3];

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

        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateBGImage.gameObject.SetActive(false);
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
        /*if (Questioner)
        {
            QuestionerText.SetActive(true);
        }
        if (Answerer)
        {
            AnswererText.SetActive(true);
        }*/
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

        StartCoroutine(TransitionEffect4());
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

        StartCoroutine(TransitionEffect4());
        yield return new WaitForSeconds(1f);

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
            yield return new WaitForSeconds(0.5f);
            _view.RPC(nameof(StartResult), RpcTarget.All);
        }
        yield return new WaitForSeconds(0.1f);

        if (QuesitionNum == 6) yield break;

        StartCoroutine(TransitionEffect4());
        yield return new WaitForSeconds(1f);

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

    //画面遷移演出
    private int num = 0;
    private IEnumerator TransitionEffect()
    {
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(true);
        for (int i = 0; i < 50; i++)
        {
            TransitionStateImage.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateImage.sprite = TransitionImage[num];
        num++;
        if (num == 3) num = 0;
        yield break;
    }

    //画面遷移演出2
    private IEnumerator TransitionEffect2()
    {
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(true);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount += 0.04f;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.3f);
        TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount -= 0.04f;
            yield return new WaitForSeconds(0.015f);
        }
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        //TransitionStateImage.sprite = TransitionImage[num];
        num++;
        if (num == 3) num = 0;
        yield break;
    }

    //画面遷移演出3
    private IEnumerator TransitionEffect3()
    {
        num++;
        if (num == 3) num = 0;

        /*TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        TransitionStateImage.fillAmount = 1f;
        TransitionStateImage.color = new Color(1, 1, 1, 0);*/
        TransitionStateImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        /*TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        TransitionStateBGImage.fillAmount = 1f;
        TransitionStateBGImage.color = new Color(1, 1, 1, 0);*/
        TransitionStateBGImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);

        TransitionStateImage.sprite = TransitionImage[num];
        TransitionStateBGImage.sprite = TransitionBGImage[num];
        /*
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
            TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
            yield return new WaitForSeconds(0.02f);
        }*/
        TransitionStateImage.fillAmount = 0;
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateImage.color = new Color(1, 1, 1, 1);
        TransitionStateBGImage.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount += 0.04f;
            TransitionStateBGImage.fillAmount += 0.04f;
            yield return new WaitForSeconds(0.02f);
        }
        TransitionStateImage.fillAmount = 1;
        TransitionStateBGImage.fillAmount = 1;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
            TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
            yield return new WaitForSeconds(0.015f);
        }
        TransitionStateImage.color = new Color(1, 1, 1, 0);
        TransitionStateBGImage.color = new Color(1, 1, 1, 0);
        TransitionStateBGImage.fillAmount = 1;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateBGImage.gameObject.SetActive(false);

        yield break;
    }

    //画面遷移演出4
    private IEnumerator TransitionEffect4()
    {
        num++;
        if (num == 3) num = 0;

        switch (num)
        {
            case 0:
                TransitionStateImage.color = new Color(1, 1, 1, 0);
                TransitionStateBGImage.color = new Color(1, 1, 1, 0);
                TransitionStateImage.fillAmount = 1f;
                TransitionStateBGImage.fillAmount = 1f;
                break;
            case 1:
                TransitionStateImage.color = new Color(1, 1, 1, 0);
                TransitionStateBGImage.color = new Color(1, 1, 1, 0);
                TransitionStateImage.fillAmount = 1f;
                TransitionStateBGImage.fillAmount = 1f;
                TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 2:
                TransitionStateImage.color = new Color(1, 1, 1, 1);
                TransitionStateBGImage.color = new Color(1, 1, 1, 1);
                TransitionStateImage.fillAmount = 0;
                TransitionStateBGImage.fillAmount = 0;
                TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("画面遷移演出ミス");
                break;
        }
        yield return new WaitForSeconds(0.01f);

        TransitionStateImage.sprite = TransitionImage[num];
        TransitionStateBGImage.sprite = TransitionBGImage[num];

        TransitionStateImage.gameObject.SetActive(true);
        TransitionStateBGImage.gameObject.SetActive(true);

        switch (num)
        {
            case 0:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.015f);
                }
                break;
            case 1:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.fillAmount -= 0.04f;
                    TransitionStateBGImage.fillAmount -= 0.04f;
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            case 2:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.fillAmount += 0.04f;
                    TransitionStateBGImage.fillAmount += 0.04f;
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.015f);
                }
                break;
            default:
                Debug.Log("画面遷移演出ミス");
                break;
        }

        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.gameObject.SetActive(false);

        yield break;
    }
}
