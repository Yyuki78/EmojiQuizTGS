using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InRoom : MonoBehaviourPunCallbacks
{
    private PhotonView _view;

    private bool isStart = false;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            //全プレイヤー取得
            Player[] Players = PhotonNetwork.PlayerList;

            //他に人がいないならreturn
            if (Players.Length <= 1) return;

            //準備完了かどうかを人数分判定し続ける
            for (int i = 0; i < Players.Length; i++)
            {
                bool hantei = Players[i].GetReady();
                if (!hantei)
                {
                    return;
                }
            }

            //全員が準備完了なら
            _view.RPC(nameof(StartMainGame), RpcTarget.All);
        }

        if (isStart && once)
        {
            StartCoroutine(CountDown());
            once = false;
        }
    }

    [PunRPC]
    private void StartMainGame()
    {
        isStart = true;
    }

    private IEnumerator CountDown()
    {
        //カウントダウン
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("ゲームスタート！");
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.MainGame);
        yield break;
    }


    public void OnClickIcon(int IconNum)
    {
        //準備完了ボタンを押せるようにする

        // アイコンを設定する

    }

    public void OnClickReadyButton()
    {
        Debug.Log("準備完了！");
        // 準備完了
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetReady(true);
        }
        // 準備完了ボタンを出す
    }
}
