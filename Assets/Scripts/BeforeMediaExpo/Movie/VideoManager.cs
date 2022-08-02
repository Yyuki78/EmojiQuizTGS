using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField, Tooltip("PlayButton �����I�u�W�F�N�g")]
    private GameObject _gameObject2;

    [SerializeField, Tooltip("SkipButton �����I�u�W�F�N�g")]
    private GameObject _gameObject3;

    [SerializeField, Tooltip("Video Player �����I�u�W�F�N�g")]
    private GameObject _gameObject;

    [SerializeField] GameObject BackGroundPanel;
    [SerializeField] GameObject IconSelectPanel;

    [SerializeField] GameObject SoundManager;
    SoundManager _sound;

    private bool once = true;//����̍Đ������x���Ă΂Ȃ����߂�bool

    public int ReturnState;//����I����ɑJ�ڂ���State�̊Ǘ��BMovieButtonScript�Ŏg�p

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
        //GameMode��Movie�Ȃ瓮����Đ�����
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
        //����̍Đ�
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
                Debug.Log("Movie���I���Ȃ�");
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
                Debug.Log("Movie���I���Ȃ�");
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
