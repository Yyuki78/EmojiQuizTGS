using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    private bool Mute = false;
    [SerializeField] GameObject MuteImage;
    public void OnClickSoundButton()
    {
        Mute = !Mute;
        if (Mute)
        {
            MuteImage.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            MuteImage.SetActive(false);
            AudioListener.volume = 1f;
        }
    }
}
