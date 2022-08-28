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

    private MovieButtons _movie;

    // Start is called before the first frame update
    void Start()
    {
        _soundButtonImage = GetComponent<Image>();
        _movie = GetComponentInParent<MovieButtons>();
        SetVolumeSlider.SetActive(false);
    }

    //���ʃ{�^���ɕt����
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

    //�X���C�_�[�ɕt����
    public void SetMaster(float volume)
    {
        _audioMixer.SetFloat("MasterVol", volume);
        _movie.ChangeVolume(volume);
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
