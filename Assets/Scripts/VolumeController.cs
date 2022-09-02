using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] int ButtonMode = 0;//0でMovieの場所 1でInRoomの場所

    public static float Volume = 0;

    [SerializeField] GameObject SetVolumeSlider;
    private Slider _slider;
    private bool isActive = false;

    [SerializeField]
    private AudioMixer _audioMixer;

    private Image _soundButtonImage;
    [SerializeField] Sprite SoundImage;
    [SerializeField] Sprite muteSoundImage;

    private MovieButtons _movie;

    // Start is called before the first frame update
    void Start()
    {
        _slider = SetVolumeSlider.GetComponent<Slider>();
        _soundButtonImage = GetComponent<Image>();
        if (ButtonMode == 0)
        {
            _movie = GetComponentInParent<MovieButtons>();
        }
        SetVolumeSlider.SetActive(false);
        SetMaster(Volume);
        _slider.value = Volume;
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
        Volume = volume;
        _audioMixer.SetFloat("MasterVol", volume);
        if (ButtonMode == 0)
        {
            _movie.ChangeVolume(volume);
        }
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
