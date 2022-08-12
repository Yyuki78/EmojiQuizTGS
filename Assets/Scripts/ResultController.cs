using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField] Image[] IconImage = new Image[5];//Icon画像を表示させるImageオブジェクト

    private ResultStaging[] _staging = new ResultStaging[5];//バー演出

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _staging = GetComponentsInChildren<ResultStaging>();
        for(int i = 0; i < 5; i++)
        {
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

        //アイコンのセット
        int i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            IconImage[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
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

        yield return new WaitForSeconds(15f);

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
