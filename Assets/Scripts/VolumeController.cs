using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] GameObject SetVolumeSlider;
    private bool isActive = false;

    [SerializeField]
    private AudioMixer _audioMixer;

    private Image _soundButtonImage;
    [SerializeField] Sprite SoundImage;
    [SerializeField] Sprite muteSoundImage;

    // Start is called before the first frame update
    void Start()
    {
        _soundButtonImage = GetComponent<Image>();
        SetVolumeSlider.SetActive(false);
    }

    //音量ボタンに付ける
    public void OnClickSoundButton()
    {
        if (!isActive)
        {
            isActive = true;
            SetVolumeSlider.SetActive(true);
        }
        else
        {
            isActive = false;
            SetVolumeSlider.SetActive(false);
        }
    }

    //スライダーに付ける
    public void SetMaster(float volume)
    {
        _audioMixer.SetFloat("MasterVol", volume);
        if (volume <= -40)
        {
            _audioMixer.SetFloat("MasterVol", -80);
            _soundButtonImage.sprite = muteSoundImage;
        }
        else
        {
            _soundButtonImage.sprite = SoundImage;
        }
    }
}
