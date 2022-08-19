using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieButtons : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
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
