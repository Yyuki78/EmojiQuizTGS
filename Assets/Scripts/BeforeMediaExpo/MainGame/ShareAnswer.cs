using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShareAnswer : MonoBehaviourPunCallbacks
{
    //正解発表のスクリプト」
    [SerializeField] GameObject MainGamePanel;
    private ThemaGenerator _themaGenerator;
    private bool once = true;
    
    [SerializeField] Image QustionerImage;
    [SerializeField] Image CorrectImage;

    [SerializeField] Image IconImage1 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage2 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage3 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage4 = null;//Emoji画像を表示させるImageオブジェクト
    public Image[] IconImage = new Image[4];

    [SerializeField] Image AnswerImage1 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image AnswerImage2 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image AnswerImage3 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image AnswerImage4 = null;//Emoji画像を表示させるImageオブジェクト
    public Image[] AnswerImage = new Image[4];

    //正解と不正解の画像
    [SerializeField] Image Correct1 = null;
    [SerializeField] Image Correct2 = null;
    [SerializeField] Image Correct3 = null;
    [SerializeField] Image Correct4 = null;
    public Image[] Correct = new Image[4];

    [SerializeField] Image inCorrect1 = null;
    [SerializeField] Image inCorrect2 = null;
    [SerializeField] Image inCorrect3 = null;
    [SerializeField] Image inCorrect4 = null;
    public Image[] inCorrect = new Image[4];

    [SerializeField] Image MasCorrect = null;
    [SerializeField] Image MasinCorrect = null;

    private int i = 0;
    private int k = 0;
    //正解数と不正解数
    private int maru = 0;
    private int batu = 0;

    //全体の正解不正解を持つbool[正解か不正解か]
    public static bool[] answer1 = new bool[10];
    public static bool[] answer2 = new bool[10];
    public static bool[] answer3 = new bool[10];
    public static bool[] answer4 = new bool[10];
    public static bool[] answer5 = new bool[10];

    [SerializeField] GameObject SoundManager;
    SoundManager _sound;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
        _sound = SoundManager.GetComponent<SoundManager>();

        IconImage[0] = IconImage1;
        IconImage[1] = IconImage2;
        IconImage[2] = IconImage3;
        IconImage[3] = IconImage4;

        AnswerImage[0] = AnswerImage1;
        AnswerImage[1] = AnswerImage2;
        AnswerImage[2] = AnswerImage3;
        AnswerImage[3] = AnswerImage4;

        Correct[0] = Correct1;
        Correct[1] = Correct2;
        Correct[2] = Correct3;
        Correct[3] = Correct4;

        inCorrect[0] = inCorrect1;
        inCorrect[1] = inCorrect2;
        inCorrect[2] = inCorrect3;
        inCorrect[3] = inCorrect4;

        QustionerImage.gameObject.SetActive(false);
        CorrectImage.gameObject.SetActive(false);
        IconImage1.gameObject.SetActive(false);
        IconImage2.gameObject.SetActive(false);
        IconImage3.gameObject.SetActive(false);
        IconImage4.gameObject.SetActive(false);
        AnswerImage1.gameObject.SetActive(false);
        AnswerImage2.gameObject.SetActive(false);
        AnswerImage3.gameObject.SetActive(false);
        AnswerImage4.gameObject.SetActive(false);
        Correct1.gameObject.SetActive(false);
        Correct2.gameObject.SetActive(false);
        Correct3.gameObject.SetActive(false);
        Correct4.gameObject.SetActive(false);
        inCorrect1.gameObject.SetActive(false);
        inCorrect2.gameObject.SetActive(false);
        inCorrect3.gameObject.SetActive(false);
        inCorrect4.gameObject.SetActive(false);
        MasCorrect.gameObject.SetActive(false);
        MasinCorrect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.MainGame)
        {
            if (MainGameManager2.mainmode == MainGameManager2.MainGameMode.ShareAnswer)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    if (once == true)
                    {
                        photonView.RPC(nameof(RpcShareAnswer), RpcTarget.All, PhotonNetwork.LocalPlayer.GetScore());
                        once = false;
                    }
                }
            }
        }
    }

    [PunRPC]
    private void RpcShareAnswer(int thema)
    {
        StartCoroutine(shareAnswer(thema));
    }

    private IEnumerator shareAnswer(int thema)
    {
        Debug.Log("正解発表を始めます");
        var players = PhotonNetwork.PlayerList;
        var players2 = PhotonNetwork.PlayerList;

        //アイコンの生成
        QustionerImage.sprite = Resources.Load<Sprite>("Image/" + thema);
        i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.1f);
            if (player.IsMasterClient) continue;
            IconImage[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
            i++;
        }
        QustionerImage.gameObject.SetActive(true);
        for(int j = 0; j < 4; j++)
        {
            IconImage[j].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        //ここから選んだ選択肢の表示
        k = 0;
        foreach (var player in players2)
        {
            if (player.IsMasterClient) continue;
            AnswerImage[k].sprite = Resources.Load<Sprite>("Image/" + player.GetChoiceNum());
            Debug.Log(player.GetChoiceNum());
            k++;
        }
        for (int j = 0; j < 4; j++)
        {
            yield return new WaitForSeconds(0.75f);
            AnswerImage[j].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        //ここから正解の表示と正解不正解の表示
        CorrectImage.sprite = Resources.Load<Sprite>("Image/" + _themaGenerator._themaNum);
        CorrectImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        k = 0;
        maru = 0;
        batu = 0;
        foreach (var player in players2)
        {
            if (player.IsMasterClient) continue;
            if(player.GetChoiceNum()== _themaGenerator._themaNum)
            {
                Debug.Log("正解");
                Correct[k].gameObject.SetActive(true);
                maru++;
            }
            else
            {
                Debug.Log("不正解");
                inCorrect[k].gameObject.SetActive(true);
                batu++;
            }

            k++;
        }
        yield return new WaitForSeconds(0.2f);
        if (maru >= batu)
        {
            Debug.Log("半分以上が正解");
            MasCorrect.gameObject.SetActive(true);
            _sound.SE4();
        }
        else
        {
            Debug.Log("過半数が不正解");
            MasinCorrect.gameObject.SetActive(true);
            _sound.SE5();
        }

        //ここから正解不正解を格納する
        k = 0;
        foreach (var player in players2)
        {
            k++;
            if (player.IsMasterClient)
            {
                if (maru >= batu)
                {
                    switch (k)
                    {
                        case 1:
                            answer1[MainGameManager2.playcount - 1] = true;
                            break;
                        case 2:
                            answer2[MainGameManager2.playcount - 1] = true;
                            break;
                        case 3:
                            answer3[MainGameManager2.playcount - 1] = true;
                            break;
                        case 4:
                            answer4[MainGameManager2.playcount - 1] = true;
                            break;
                        case 5:
                            answer5[MainGameManager2.playcount - 1] = true;
                            break;
                    }
                }
                else
                {
                    switch (k)
                    {
                        case 1:
                            answer1[MainGameManager2.playcount - 1] = false;
                            break;
                        case 2:
                            answer2[MainGameManager2.playcount - 1] = false;
                            break;
                        case 3:
                            answer3[MainGameManager2.playcount - 1] = false;
                            break;
                        case 4:
                            answer4[MainGameManager2.playcount - 1] = false;
                            break;
                        case 5:
                            answer5[MainGameManager2.playcount - 1] = false;
                            break;
                    }
                }
                continue;
            }
            if (player.GetChoiceNum() == _themaGenerator._themaNum)
            {
                switch (k)
                {
                    case 1:
                        answer1[MainGameManager2.playcount - 1] = true;
                        break;
                    case 2:
                        answer2[MainGameManager2.playcount - 1] = true;
                        break;
                    case 3:
                        answer3[MainGameManager2.playcount - 1] = true;
                        break;
                    case 4:
                        answer4[MainGameManager2.playcount - 1] = true;
                        break;
                    case 5:
                        answer5[MainGameManager2.playcount - 1] = true;
                        break;
                }
            }
            else
            {
                switch (k)
                {
                    case 1:
                        answer1[MainGameManager2.playcount - 1] = false;
                        break;
                    case 2:
                        answer2[MainGameManager2.playcount - 1] = false;
                        break;
                    case 3:
                        answer3[MainGameManager2.playcount - 1] = false;
                        break;
                    case 4:
                        answer4[MainGameManager2.playcount - 1] = false;
                        break;
                    case 5:
                        answer5[MainGameManager2.playcount - 1] = false;
                        break;
                }
            }
        }

        yield return new WaitForSeconds(3.8f);
        Debug.Log("正解発表を終了します");
        yield break;
    }

    public void Init()
    {
        QustionerImage.gameObject.SetActive(false);
        CorrectImage.gameObject.SetActive(false);
        IconImage1.gameObject.SetActive(false);
        IconImage2.gameObject.SetActive(false);
        IconImage3.gameObject.SetActive(false);
        IconImage4.gameObject.SetActive(false);
        AnswerImage1.gameObject.SetActive(false);
        AnswerImage2.gameObject.SetActive(false);
        AnswerImage3.gameObject.SetActive(false);
        AnswerImage4.gameObject.SetActive(false);
        Correct1.gameObject.SetActive(false);
        Correct2.gameObject.SetActive(false);
        Correct3.gameObject.SetActive(false);
        Correct4.gameObject.SetActive(false);
        inCorrect1.gameObject.SetActive(false);
        inCorrect2.gameObject.SetActive(false);
        inCorrect3.gameObject.SetActive(false);
        inCorrect4.gameObject.SetActive(false);
        MasCorrect.gameObject.SetActive(false);
        MasinCorrect.gameObject.SetActive(false);

        once = true;
    }
}
