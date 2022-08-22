using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieButtons : MonoBehaviour
{
    [SerializeField]
    private string relativePath;

    private VideoPlayer _videoPlayer;

    private bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.source = VideoSource.Url;
        _videoPlayer.url = Application.streamingAssetsPath + "/" + relativePath;
        _videoPlayer.prepareCompleted += PrepareCompleted;
        _videoPlayer.Prepare();
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
    }

    public void PlayorStop()
    {
        if (play)
        {
            _videoPlayer.Pause();
            play = false;
        }
        else
        {
            _videoPlayer.Play();
            play = true;
        }
    }

    public void SkipMovie()
    {
        _videoPlayer.Stop();
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.RoomSelect);
    }
}
