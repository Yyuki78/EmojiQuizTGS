using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InRoom : MonoBehaviourPunCallbacks
{
    private PhotonView _view;

    //ゲーム開始判定用
    private bool isStart = false;
    private bool once = true;
    private bool once2 = true;

    //アイコン選択画面の点滅用
    private bool isFlashingIcon = false;
    private bool plus1 = false;

    //準備完了ボタンの点滅用
    private bool isFlashing = false;
    private bool plus = false;

    [SerializeField] Image StartPanel;//黒い画面から元に戻るためのPanel

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    [SerializeField] Image myIconImage; //自分のアイコン画像
    [SerializeField] GameObject noSelectPanel;//アイコン以外を選択できなくするパネル
    [SerializeField] Image IconSelectImage;//アイコン選択画面の画像
    [SerializeField] Image ReadyButtonImage;//準備完了ボタンの画像
    [SerializeField] GameObject myCheckMarkImage;//自分の準備完了を示すチェックマーク

    [SerializeField] GameObject[] otherInfo = new GameObject[4];//他の人がいるかどうか
    [SerializeField] Image[] otherIconImages = new Image[4];//他の人のアイコン
    [SerializeField] GameObject[] otherCheckImages = new GameObject[4];//他の人のチェックマーク

    [SerializeField] GameObject[] alreadySelectIconImages = new GameObject[6];//既に選択されたアイコンを隠す用

    //音系
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();
        _audio = AudioManager.GetComponent<AudioManager>();
        StartPanel.gameObject.SetActive(true);
        noSelectPanel.SetActive(true);
        myCheckMarkImage.SetActive(false);
        for(int i = 0; i < 4; i++)
        {
            otherInfo[i].SetActive(false);
            otherCheckImages[i].SetActive(false);
        }
        for(int i = 0; i < 6; i++)
        {
            alreadySelectIconImages[i].SetActive(false);
        }
        isFlashingIcon = true;

        PhotonNetwork.LocalPlayer.SetScore(10);

        StartCoroutine(StartEffect());
        //StartCoroutine(ShareIconInfo());
        StartCoroutine(SharePlayerInfo());
    }

    // Update is called once per frame
    void Update()
    {
        //アイコン選択パネルの点滅
        if (isFlashingIcon)
        {
            if (IconSelectImage.color.a >= 0.9f)
            {
                plus1 = false;
            }
            if (IconSelectImage.color.a <= 0.5f)
            {
                plus1 = true;
            }
            if (!plus1)
            {
                IconSelectImage.color -= new Color(0, 0, 0, 0.0075f);
            }
            else
            {
                IconSelectImage.color += new Color(0, 0, 0, 0.0075f);
            }
        }

        //準備完了ボタンの点滅
        if (isFlashing)
        {
            if (ReadyButtonImage.color.a >= 1)
            {
                plus = false;
            }
            if (ReadyButtonImage.color.a <= 0.75f)
            {
                plus = true;
            }
            if (!plus)
            {
                ReadyButtonImage.color -= new Color(0, 0, 0, 0.0075f);
            }
            else
            {
                ReadyButtonImage.color += new Color(0, 0, 0, 0.0075f);
            }
        }


        //ゲーム開始の判定
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
            PhotonNetwork.CurrentRoom.IsOpen = false;
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

        var players = PhotonNetwork.PlayerList;
        yield return new WaitForSeconds(0.01f);

        //それぞれの番号を設定
        int i = 0;
        foreach (var player in players)
        {
            if (PhotonNetwork.LocalPlayer == player)
            {
                PhotonNetwork.LocalPlayer.UpdatePlayerNum(i);
            }
            i++;
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("ゲームスタート！");
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.MainGame);
        yield break;
    }


    public void OnClickIcon()
    {
        _audio.SE1();

        //準備完了ボタンを押せるようにする
        noSelectPanel.SetActive(false);

        // アイコンを設定する
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("選択されたのは" + button_ob.name);
        int x = int.Parse(button_ob.name);
        Debug.Log("画像番号は" + x);
        PhotonNetwork.LocalPlayer.SetScore(x);

        myIconImage.sprite = Resources.Load<Sprite>("IconImage/" + x);

        if (once2)
        {
            once2 = false;
            IconSelectImage.color = new Color(1, 1, 1, 0.8f);
            isFlashingIcon = false;
            isFlashing = true;
        }
    }

    public void OnClickReadyButton()
    {
        Debug.Log("準備完了！");
        _audio.SE1();

        // 準備完了
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetReady(true);
        }
        // 準備完了マークを出す
        isFlashing = false;
        ReadyButtonImage.color = new Color(1, 1, 1, 1);

        myCheckMarkImage.SetActive(true);
    }

    //既に選択されたアイコンを選択不可にする
    private IEnumerator ShareIconInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            var players = PhotonNetwork.PlayerList;

            for (int i = 1; i < 7; i++)
            {
                foreach (var player in players)
                {
                    if (player.GetScore() == i)
                    {
                        alreadySelectIconImages[i].SetActive(true);
                    }
                    else
                    {
                        alreadySelectIconImages[i].SetActive(false);
                    }
                }
            }
        }
    }

    //自分以外の人のアイコン、チェックマークの共有
    private IEnumerator SharePlayerInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            //他の全プレイヤー取得
            Player[] otherPlayers = PhotonNetwork.PlayerListOthers;

            for (int i = 0; i < otherPlayers.Length; i++)
            {
                otherInfo[i].SetActive(true);
            }

            switch (otherPlayers.Length)
            {
                case 0:
                    Debug.Log("一人");
                    break;
                case 1:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    otherInfo[1].SetActive(false);
                    otherInfo[2].SetActive(false);
                    otherInfo[3].SetActive(false);
                    break;
                case 2:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    otherInfo[2].SetActive(false);
                    otherInfo[3].SetActive(false);
                    break;
                case 3:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    otherIconImages[2].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[2].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    if (otherPlayers[2].GetReady())
                    {
                        otherCheckImages[2].SetActive(true);
                    }
                    otherInfo[3].SetActive(false);
                    break;
                case 4:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    otherIconImages[2].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[2].GetScore());
                    otherIconImages[3].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[3].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    if (otherPlayers[2].GetReady())
                    {
                        otherCheckImages[2].SetActive(true);
                    }
                    if (otherPlayers[3].GetReady())
                    {
                        otherCheckImages[3].SetActive(true);
                    }
                    break;
                default:
                    Debug.Log("入っている人数がおかしいです");
                    break;
            }
        }
    }

    // 他のプレイヤーが退室した時
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        
        var players = PhotonNetwork.PlayerList;
        int i = 0;
        foreach (var player in players)
        {
            if (otherPlayer == player)
            {
                otherInfo[i].SetActive(false);
                otherIconImages[i].sprite = Resources.Load<Sprite>("IconImage/10");
                otherCheckImages[i].SetActive(false);
            }
            i++;
        }
    }

    private IEnumerator StartEffect()
    {
        for(int i = 0; i < 50; i++)
        {
            StartPanel.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        StartPanel.gameObject.SetActive(false);
        yield break;
    }
}
