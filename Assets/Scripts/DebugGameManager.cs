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

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    //��ʑJ�ډ��o�p�摜
    [SerializeField] GameObject TransitionImage;

    void Awake()
    {
        Instance = this;
        SetCurrentState(GameMode.Start);
        _audio = AudioManager.GetComponent<AudioManager>();
        TransitionImage.SetActive(false);
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

    //���U���g�ڍs���Ɏg�p
    public void GoResult()
    {
        StartCoroutine(TransitionResult());
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
                ResultAction();
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

        _audio.StopBGM();

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

        _audio.StopBGM();
        _audio.BGM2();
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

        _audio.StopBGM();
    }

    // Result�ɂȂ����Ƃ��̏���
    void ResultAction()
    {
        Debug.Log("ResultMode");
    }

    private IEnumerator TransitionResult()
    {
        TransitionImage.transform.localPosition = new Vector3(1350, 0, 0);
        TransitionImage.SetActive(true);

        _audio.StopBGM();
        
        //���U���g�ڍs���̌��ʉ�������Ȃ炱��

        for (int i = 0; i < 27; i++)
        {
            TransitionImage.transform.localPosition -= new Vector3(50, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }

        StartPanel.SetActive(false);
        MoviePanel.SetActive(false);
        RoomSelectPanel.SetActive(false);
        InRoomPanel.SetActive(false);
        MainGamePanel.SetActive(false);
        ResultPanel.SetActive(true);

        SetCurrentState(DebugGameManager.GameMode.Result);

        for (int i = 0; i < 27; i++)
        {
            TransitionImage.transform.localPosition -= new Vector3(50, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.2f);
        TransitionImage.SetActive(false);
        _audio.BGM4();

        yield break;
    }

    private IEnumerator WaitMovie()
    {
        //���掞�ԕ��҂��Ă��烋�[���I���Ɉڍs����
        yield return new WaitForSeconds(18f);
        if (currentGameState != GameMode.Movie) yield break;
        SetCurrentState(DebugGameManager.GameMode.RoomSelect);
        yield break;
    }
}
