using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Start,
        Movie,
        IconSelect,
        RoomSelect,
        InRoom,
        MainGame,
        Result
    }

    public static GameManager Instance;

    // 現在の状態
    private GameMode currentGameState;

    //MovieModeかどうか
    public bool IsMovie => GameMode.Movie == currentGameState;

    [SerializeField] GameObject MoviePanel;
    [SerializeField] GameObject IconSelectPanel;
    [SerializeField] GameObject RoomSelectPanel1;
    [SerializeField] GameObject RoomSelectPanel2;
    [SerializeField] GameObject RoomSelectPanel3;
    [SerializeField] GameObject RoomSelectPanel4;
    [SerializeField] GameObject InRoomPanel;
    [SerializeField] GameObject MainGamePanel;

    [SerializeField] GameObject SoundManager;
    SoundManager _sound;

    void Awake()
    {
        Instance = this;
        SetCurrentState(GameMode.Start);
        //SetCurrentState(GameMode.RoomSelect);
        Debug.Log(GetCurrentState());
        Debug.Log("GameManager_Awake");
        _sound = SoundManager.GetComponent<SoundManager>();
        _sound.BGM1();
    }


    // 外からこのメソッドを使って状態を変更
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
            case GameMode.Movie:
                MovieAction();
                break;
            case GameMode.IconSelect:
                IconSelectAction();
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
                EndAction();
                break;
            default:
                break;
        }
    }

    // Movieになったときの処理
    void MovieAction()
    {
        _sound.StopA();
        Debug.Log("MovieMode");
        MoviePanel.SetActive(true);
        IconSelectPanel.SetActive(false);
        RoomSelectPanel1.SetActive(false);
        RoomSelectPanel2.SetActive(false);
        RoomSelectPanel3.SetActive(false);
        RoomSelectPanel4.SetActive(false);
    }

    // IconSelectになったときの処理
    void IconSelectAction()
    {
        Debug.Log("IconSelectMode");
        MoviePanel.SetActive(false);
        IconSelectPanel.SetActive(true);
    }
    // RoomSelectになったときの処理
    void RoomSelectAction()
    {
        Debug.Log("RoomSelectMode");
        MoviePanel.SetActive(false);
        IconSelectPanel.SetActive(true);
        RoomSelectPanel1.SetActive(true);
        RoomSelectPanel2.SetActive(true);
        RoomSelectPanel3.SetActive(true);
        RoomSelectPanel4.SetActive(true);
        InRoomPanel.SetActive(false);
    }
    void InRoomAction()
    {
        IconSelectPanel.SetActive(false);
        RoomSelectPanel1.SetActive(false);
        RoomSelectPanel2.SetActive(false);
        RoomSelectPanel3.SetActive(false);
        RoomSelectPanel4.SetActive(false);
        InRoomPanel.SetActive(true);
        MainGamePanel.SetActive(false);
    }
    void MainGameAction()
    {
        _sound.StopA();
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(true);
        MainGameManager.mainmode = MainGameManager.MainGameMode.LoadGame;
    }
    // Endになったときの処理
    void EndAction()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
