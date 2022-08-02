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

    // ���݂̏��
    private GameMode currentGameState;

    //MovieMode���ǂ���
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


    // �O���炱�̃��\�b�h���g���ď�Ԃ�ύX
    public void SetCurrentState(GameMode state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    public GameMode GetCurrentState()
    {
        return currentGameState;
    }

    // ��Ԃ��ς�����牽�����邩
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

    // Movie�ɂȂ����Ƃ��̏���
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

    // IconSelect�ɂȂ����Ƃ��̏���
    void IconSelectAction()
    {
        Debug.Log("IconSelectMode");
        MoviePanel.SetActive(false);
        IconSelectPanel.SetActive(true);
    }
    // RoomSelect�ɂȂ����Ƃ��̏���
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
    // End�ɂȂ����Ƃ��̏���
    void EndAction()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
