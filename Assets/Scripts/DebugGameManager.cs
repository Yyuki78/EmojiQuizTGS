using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameManager : MonoBehaviour
{
    //全体の状態を管理するManager
    public enum GameMode
    {
        Start,
        Movie,
        RoomSelect,
        InRoom,
        MainGame,
        Result
    }

    public static DebugGameManager Instance;

    // 現在の状態
    private GameMode currentGameState;

    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject MoviePanel;
    [SerializeField] GameObject RoomSelectPanel;
    [SerializeField] GameObject InRoomPanel;
    [SerializeField] GameObject MainGamePanel;
    [SerializeField] GameObject ResultPanel;

    void Awake()
    {
        Instance = this;
        SetCurrentState(GameMode.Start);
    }

    // 外からこのメソッドを使って状態を変更
    //Start→Movie 動画再生ボタン ChangeStateButtonsで変更
    //Movie→RoomSelect　動画再生後に自動移行
    //RoomSelect→InRoom　部屋選択ボタン
    //InRoom→MainGame　全員が準備完了にマスターからのRPC
    //MainGame→Result　5問終了後にマスターからのRPC
    //Result→Start　リザルト表示後に自動移行
    public void SetCurrentState(GameMode state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    public GameMode GetCurrentState()
    {
        return currentGameState;
    }

    // 状態が変わったら何をするか
    void OnGameStateChanged(GameMode state)
    {
        switch (state)
        {
            case GameMode.Start:
                StartAction();
                break;
            case GameMode.Movie:
                MovieAction();
                break;
            case GameMode.RoomSelect:
                RoomSelectAction();
                break;
            case GameMode.InRoom:
                InRoomAction();
                break;
            case GameMode.MainGame:
                MainGameAction();
                break;
            case GameMode.Result:
                break;
            default:
                break;
        }
    }

    // Startになったときの処理
    void StartAction()
    {
        Debug.Log("Start");
        StartPanel.SetActive(true);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        ResultPanel.SetActive(false);
    }

    // Movieになったときの処理
    void MovieAction()
    {
        Debug.Log("MovieMode");
        StartPanel.SetActive(false);
        MoviePanel.SetActive(true);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        ResultPanel.SetActive(false);

        StartCoroutine(WaitMovie());
    }

    // RoomSelectになったときの処理
    void RoomSelectAction()
    {
        Debug.Log("RoomSelectMode");
        StartPanel.SetActive(false);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(true);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        ResultPanel.SetActive(false);
    }

    // InRoomになったときの処理
    void InRoomAction()
    {
        Debug.Log("InRoomMode");
        StartPanel.SetActive(false);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(true);
        MainGamePanel.SetActive(false);
        ResultPanel.SetActive(false);
    }

    // MainGameになったときの処理
    void MainGameAction()
    {
        Debug.Log("MainGameMode");
        StartPanel.SetActive(false);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(true);
        ResultPanel.SetActive(false);
        MainGameManager.mainmode = MainGameManager.MainGameMode.LoadGame;
    }

    // Resultになったときの処理
    void ResultAction()
    {
        Debug.Log("ResultMode");
        StartPanel.SetActive(false);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(true);
        ResultPanel.SetActive(false);
        MainGameManager.mainmode = MainGameManager.MainGameMode.LoadGame;
    }

    private IEnumerator WaitMovie()
    {
        //動画時間分待ってからルーム選択に移行する
        yield return new WaitForSeconds(3f);
        SetCurrentState(DebugGameManager.GameMode.RoomSelect);
        yield break;
    }
}
