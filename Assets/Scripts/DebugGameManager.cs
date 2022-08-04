using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameManager : MonoBehaviour
{
    //�S�̂̏�Ԃ��Ǘ�����Manager
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

    // ���݂̏��
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

    // �O���炱�̃��\�b�h���g���ď�Ԃ�ύX
    //Start��Movie ����Đ��{�^�� ChangeStateButtons�ŕύX
    //Movie��RoomSelect�@����Đ���Ɏ����ڍs
    //RoomSelect��InRoom�@�����I���{�^��
    //InRoom��MainGame�@�S�������������Ƀ}�X�^�[�����RPC
    //MainGame��Result�@5��I����Ƀ}�X�^�[�����RPC
    //Result��Start�@���U���g�\����Ɏ����ڍs
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

    // Start�ɂȂ����Ƃ��̏���
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

    // Movie�ɂȂ����Ƃ��̏���
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

    // RoomSelect�ɂȂ����Ƃ��̏���
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

    // InRoom�ɂȂ����Ƃ��̏���
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

    // MainGame�ɂȂ����Ƃ��̏���
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

    // Result�ɂȂ����Ƃ��̏���
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
        //���掞�ԕ��҂��Ă��烋�[���I���Ɉڍs����
        yield return new WaitForSeconds(3f);
        SetCurrentState(DebugGameManager.GameMode.RoomSelect);
        yield break;
    }
}
