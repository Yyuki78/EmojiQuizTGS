using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MainGameでないならReturn
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;


    }

    //役割通知　マスターからのRPCで実行
    private IEnumerator InformRole()
    {
        yield return new WaitForSeconds(0.1f);
        //変数の初期化

        //問題数の増加

        //マスターは問題の設定及び配布

        //出題者・解答者を問題数から決定

        //役割に応じた画面表示

        //カウントダウン表示
    }

    //ゲーム中　マスターからのRPCで実行
    private IEnumerator QuestionTime()
    {
        yield return new WaitForSeconds(0.1f);
        //選択肢表示
        //カウントダウン表示

    }

    //リザルト　マスターからのRPCで実行
    private IEnumerator ReportAnswer()
    {
        yield return new WaitForSeconds(0.1f);
        //プレイヤーの解答表示

        //答えの表示

        //次へのカウントダウン表示

        //マスターは問題数が5ならリザルトへ行かせる
    }
}
