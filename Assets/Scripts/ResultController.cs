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

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _staging = GetComponentsInChildren<ResultStaging>();
        for(int i = 0; i < 5; i++)
        {
            IconFlameImage[i].sprite = otherIconFlame;
            IconFlameImage[i].gameObject.SetActive(false);
            IconImage[i].gameObject.SetActive(false);
        }
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
                BarPos[0].transform.localPosition = new Vector3(0, -75, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -325, 0);
                break;
            case 3:
                BarPos[0].transform.localPosition = new Vector3(0, 0, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -400, 0);
                break;
            case 4:
                BarPos[0].transform.localPosition = new Vector3(0, 50, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -125, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -300, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -475, 0);
                break;
            case 5:
                BarPos[0].transform.localPosition = new Vector3(0, 100, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -50, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -350, 0);
                BarPos[4].transform.localPosition = new Vector3(0, -500, 0);
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
            }
            IconFlameImage[i].gameObject.SetActive(true);
            IconImage[i].gameObject.SetActive(true);
            i++;
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("ShowResult");
        StartCoroutine(_staging[0].Staging(ReportAnswer.answer1, 1));
        StartCoroutine(_staging[1].Staging(ReportAnswer.answer2, 2));
        if (players.Length > 2)
        {
            StartCoroutine(_staging[2].Staging(ReportAnswer.answer3, 3));
        }
        if (players.Length > 3)
        {
            StartCoroutine(_staging[3].Staging(ReportAnswer.answer4, 4));
        }
        if (players.Length > 4)
        {
            StartCoroutine(_staging[4].Staging(ReportAnswer.answer5, 5));
        }

        while (!_staging[0].isFinishResult)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);

        //カウントダウン
        yield return new WaitForSeconds(5f);


        //部屋から退出し、シーンをリロードする
        Debug.Log("タイトルに戻ります");

        //暗転などの演出あってもいい

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
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene("MainScene");
    }
}
