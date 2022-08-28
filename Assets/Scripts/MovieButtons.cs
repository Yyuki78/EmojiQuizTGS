using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieButtons : MonoBehaviour
{
    [SerializeField]
    private string relativePath;

    private VideoPlayer _videoPlayer;

    private AudioSource _audio;

    private bool play = true;

    [SerializeField] Image PlayStopButtonImage;

    [SerializeField] Sprite playImage;
    [SerializeField] Sprite stopImage;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _videoPlayer = GetComponent<VideoPlayer>();

        /*
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        _videoPlayer.EnableAudioTrack(0, true);

        _videoPlayer.SetTargetAudioSource(0, _audio);
        */
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetDirectAudioVolume(0, 0.4f);

        _videoPlayer.source = VideoSource.Url;
        _videoPlayer.url = Application.streamingAssetsPath + "/" + relativePath;
        _videoPlayer.prepareCompleted += PrepareCompleted;
        _videoPlayer.Prepare();
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
        _videoPlayer.loopPointReached += FinishPlayingVideo;
    }

    //���悪�I�����
    public void FinishPlayingVideo(VideoPlayer vp)
    {
        _videoPlayer.Stop();

        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.RoomSelect);
    }

    //����̈ꎞ��~�E�ĊJ
    public void PlayorStop()
    {
        if (play)
        {
            PlayStopButtonImage.sprite = playImage;
            _videoPlayer.Pause();
            play = false;
        }
        else
        {
            PlayStopButtonImage.sprite = stopImage;
            _videoPlayer.Play();
            play = true;
        }
    }

    //������X�L�b�v
    public void SkipMovie()
    {
        _videoPlayer.Stop();
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.RoomSelect);
    }

    //����̉��ʂ�ύX
    public void ChangeVolume(float vol)
    {
        float v = (vol + 40) / 60;
        if (v > 0.6f)
        {
            v -= 0.4f;
        }
        else
        {
            v *= 0.65f;
        }
        _videoPlayer.SetDirectAudioVolume(0, v);
    }
}
