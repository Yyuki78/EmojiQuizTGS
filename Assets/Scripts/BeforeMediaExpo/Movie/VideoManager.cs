using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField, Tooltip("PlayButton を持つオブジェクト")]
    private GameObject _gameObject2;

    [SerializeField, Tooltip("SkipButton を持つオブジェクト")]
    private GameObject _gameObject3;

    [SerializeField, Tooltip("Video Player を持つオブジェクト")]
    private GameObject _gameObject;

    [SerializeField] GameObject BackGroundPanel;
    [SerializeField] GameObject IconSelectPanel;

    [SerializeField] GameObject SoundManager;
    SoundManager _sound;

    private bool once = true;//動画の再生を何度も呼ばないためのbool

    public int ReturnState;//動画終了後に遷移するStateの管理。MovieButtonScriptで使用

    VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        _videoPlayer = _gameObject.GetComponent<VideoPlayer>();
        _gameObject3.SetActive(false);
        _sound = SoundManager.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameModeがMovieなら動画を再生する
        if (GameManager.Instance.IsMovie && once == true)
        {
            PlayMovie();
        }/*
        else
        {
            ResetMovie();
        }*/
    }

    void PlayMovie()
    {
        once = false;
        //動画の再生
        BackGroundPanel.SetActive(false);
        _gameObject2.SetActive(false);
        _gameObject3.SetActive(true);
        _gameObject.SetActive(true);
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += FinishPlayingVideo;
    }

    public void FinishPlayingVideo(VideoPlayer vp)
    {
        once = true;
        _videoPlayer.Stop();

        _sound.BGM1();

        GoNextState();
    }

    void GoNextState()
    {
        switch (ReturnState)
        {
            case 0:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
                break;
            case 1:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
                break;
            case 2:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.RoomSelect);
                break;
            case 3:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
                break;
            default:
                Debug.Log("Movieが終わらない");
                break;
        }
        
    }

    public void SkipVideo()
    {
        _videoPlayer.Stop();
        _sound.BGM1();
        switch (ReturnState)
        {
            case 0:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
                break;
            case 1:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
                break;
            case 2:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.RoomSelect);
                break;
            case 3:
                GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
                break;
            default:
                Debug.Log("Movieが終わらない");
                break;
        }
        once = true;
    }

    public void ResetMovie()
    {
        _videoPlayer.Pause();
        _gameObject.SetActive(false);
        _gameObject3.gameObject.SetActive(false);
    }
}
