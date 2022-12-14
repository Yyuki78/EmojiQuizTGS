using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField] Image[] IconFlameImage = new Image[5];//アイコンフレーム画像
    [SerializeField] Sprite myIconFlame;//自分のアイコンフレーム
    [SerializeField] Sprite otherIconFlame;//他人のアイコンフレーム

    [SerializeField] Image[] IconImage = new Image[5];//Icon画像を表示させるImageオブジェクト

    private ResultStaging[] _staging = new ResultStaging[5];//バー演出

    [SerializeField] GameObject[] BarPos = new GameObject[5];//それぞれのバー・アイコンの高さ(参加者の数で変わる)

    [SerializeField] Image CountdownImage;//タイトルに戻る際のカウントダウン演出用

    [SerializeField] GameObject ConfettisParticle;//紙吹雪

    [SerializeField] Image FinishRoom;//最後の家
    [SerializeField] GameObject DoorMassObj;//最後に閉めるドア
    [SerializeField] Image BlackOutPanel;//最後に暗転するパネル

    private bool[] isMine = new bool[5];//自分のアイコンのバーかどうか ResultStagingに受け渡す
    private bool once = true;

    //音系
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = AudioManager.GetComponent<AudioManager>();
        _staging = GetComponentsInChildren<ResultStaging>();
        for(int i = 0; i < 5; i++)
        {
            IconFlameImage[i].sprite = otherIconFlame;
            IconFlameImage[i].gameObject.SetActive(false);
            IconImage[i].gameObject.SetActive(false);
            isMine[i] = false;
        }
        CountdownImage.fillAmount = 0;
        CountdownImage.gameObject.SetActive(false);
        ConfettisParticle.SetActive(false);
        FinishRoom.gameObject.SetActive(true);
        DoorMassObj.SetActive(true);
        BlackOutPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() == DebugGameManager.GameMode.Result)
        {
            if (once)
            {
                StartCoroutine(Result());
                once = false;
            }
        }
    }

    private IEnumerator Result()
    {
        var players = PhotonNetwork.PlayerList;
        yield return new WaitForSeconds(0.01f);

        switch (players.Length)
        {
            case 2:
                BarPos[0].transform.localPosition = new Vector3(0, -100, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -300, 0);
                break;
            case 3:
                BarPos[0].transform.localPosition = new Vector3(0, -50, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -350, 0);
                break;
            case 4:
                BarPos[0].transform.localPosition = new Vector3(0, -25, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -142, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -258, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -375, 0);
                break;
            case 5:
                BarPos[0].transform.localPosition = new Vector3(0, 0, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -100, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -300, 0);
                BarPos[4].transform.localPosition = new Vector3(0, -400, 0);
                break;
            default:
                Debug.Log("参加者の数がおかしいです");
                break;
        }


        yield return new WaitForSeconds(0.01f);

        //アイコンのセット
        int i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            IconImage[i].sprite = Resources.Load<Sprite>("IconImage/" + player.GetScore());
            if (player == PhotonNetwork.LocalPlayer)
            {
                IconFlameImage[i].sprite = myIconFlame;
                isMine[i] = true;
            }
            IconFlameImage[i].gameObject.SetActive(true);
            IconImage[i].gameObject.SetActive(true);
            i++;
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("ShowResult");
        StartCoroutine(_staging[0].Staging(ReportAnswer.answer1, 1, isMine[0]));
        StartCoroutine(_staging[1].Staging(ReportAnswer.answer2, 2, isMine[1]));
        if (players.Length > 2)
        {
            StartCoroutine(_staging[2].Staging(ReportAnswer.answer3, 3, isMine[2]));
        }
        if (players.Length > 3)
        {
            StartCoroutine(_staging[3].Staging(ReportAnswer.answer4, 4, isMine[3]));
        }
        if (players.Length > 4)
        {
            StartCoroutine(_staging[4].Staging(ReportAnswer.answer5, 5, isMine[4]));
        }

        while (!_staging[0].isFinishResult)
        {
            yield return null;
        }
        _audio.SE12();
        yield return new WaitForSeconds(0.5f);
        ConfettisParticle.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        CountdownImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        //_audio.SE10();

        //カウントダウン
        for (int j = 0; j < 100; j++)
        {
            CountdownImage.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }

        //部屋から退出し、シーンをリロードする
        Debug.Log("タイトルに戻ります");
        _audio.ResetSE();

        /*
        //ドアが閉まる演出あってもいい なくなりました
        for (int j = 0; j < 75; j++)
        {
            this.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int j = 0; j < 90; j++)
        {
            this.gameObject.transform.localScale -= new Vector3(0.002f, 0.002f, 0);
            DoorMassObj.transform.localEulerAngles += new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.02f);
        }*/

        //暗転
        BlackOutPanel.gameObject.SetActive(true);
        for (int j = 0; j < 50; j++)
        {
            BlackOutPanel.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.015f);
        }
        yield return new WaitForSeconds(0.01f);

        //準備完了をリセット
        PhotonNetwork.LocalPlayer.SetReady(false);
        yield return new WaitForSeconds(0.01f);

        //アイコンをリセット
        PhotonNetwork.LocalPlayer.SetScore(0);
        yield return new WaitForSeconds(0.01f);

        //選択した数字をリセット
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);
        yield return new WaitForSeconds(0.01f);

        // ルームから退出する
        PhotonNetwork.LeaveRoom();

        yield return new WaitForSeconds(0.01f);
        PhotonNetwork.Disconnect();

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainScene");
    }
}
