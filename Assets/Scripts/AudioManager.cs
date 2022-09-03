using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    [SerializeField] private AudioClip bgm1;//�^�C�g��
    [SerializeField] private AudioClip bgm2;//������
    [SerializeField] private AudioClip bgm3;//�Q�[����
    [SerializeField] private AudioClip bgm4;//���U���g

    [SerializeField] private AudioClip se1;//�N���b�N��
    [SerializeField] private AudioClip se2;//�o���
    [SerializeField] private AudioClip se3;//�𓚎�
    [SerializeField] private AudioClip se4;//����
    [SerializeField] private AudioClip se5;//�s����
    [SerializeField] private AudioClip se6;//�h�������[��
    [SerializeField] private AudioClip se7;//�h�A�K�`���K�`��
    [SerializeField] private AudioClip se8;//�o�[���L�т�
    [SerializeField] private AudioClip se9;//��ʑJ�ډ��o1
    [SerializeField] private AudioClip se10;//��ʑJ�ډ��o2
    [SerializeField] private AudioClip se11;//�h�A�̊J����

    private bool once = true;

    private void Start()
    {
        
    }

    //�v���C���[����ʂɃt�H�[�J�X����
    void OnApplicationFocus(bool hasFocus)
    {
        if (once && !bgm.isPlaying && DebugGameManager.Instance.GetCurrentState() == DebugGameManager.GameMode.Start)
        {
            once = false;
            BGM1();
        }
    }

    //DebugGameManger�Ŏg�p
    public void BGM1()
    {
        bgm.clip = bgm1;
        bgm.volume = 0.2f;
        bgm.Play();
    }

    //DebugGameManger�Ŏg�p
    public void BGM2()
    {
        bgm.clip = bgm2;
        bgm.volume = 0.05f;
        bgm.Play();
    }

    //MainGameController�Ŏg�p
    public void BGM3()
    {
        bgm.volume = 0.2f;
        bgm.PlayOneShot(bgm3);
    }

    //DebugGameManger�Ŏg�p
    public void BGM4()
    {
        bgm.volume = 0.15f;
        bgm.PlayOneShot(bgm4);
    }

    //�S�Ă�BGM���~�߂� DebugGameManger�Ŏg�p
    public void StopBGM()
    {
        bgm.Stop();
    }

    //�N���b�N InRoom,DebugGameManger,ChoicesButton�Ŏg�p
    public void SE1()
    {
        se.volume = 0.2f;
        se.PlayOneShot(se1);
    }

    //�o��� MainGameController�Ŏg�p
    public void SE2()
    {
        se.volume = 0.1f;
        se.PlayOneShot(se2);
    }

    //�𓚎� MainGameController�Ŏg�p
    public void SE3()
    {
        se.volume = 0.1f;
        se.PlayOneShot(se3);
    }

    //���� ReportAnswer�Ŏg�p
    public void SE4()
    {
        se.volume = 0.15f;
        se.PlayOneShot(se4);
    }

    //�s���� ReportAnswer�Ŏg�p
    public void SE5()
    {
        se.volume = 0.15f;
        se.PlayOneShot(se5);
    }

    //�h�������[�� ReportAnswer�Ŏg�p
    public void SE6()
    {
        se.volume = 0.5f;
        se.PlayOneShot(se6);
    }

    //�h�A�K�`���K�`�� ReportAnswer�Ŏg�p
    public void SE7()
    {
        se.volume = 0.75f;
        se.PlayOneShot(se7);
    }

    //�o�[���L�т� ReportAnswer�Ŏg�p
    public void SE8()
    {
        se.volume = 0.2f;
        se.PlayOneShot(se8);
    }

    //��ʑJ�ډ��o1 ReportAnswer�Ŏg�p
    public void SE9()
    {
        se.pitch = 1.0f;
        se.volume = 0.6f;
        se.PlayOneShot(se9);
        StartCoroutine(SE9effect());
    }
    private IEnumerator SE9effect()
    {
        for(int i = 0; i < 20; i++)
        {
            se.volume -= 0.025f;
            se.pitch += 0.025f;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    //��ʑJ�ډ��o2
    public void SE10()
    {
        se.pitch = 1.0f;
        se.volume = 0.2f;
        se.PlayOneShot(se10);
    }

    //�����ɓ��鎞�̃h�A���J���� RoomSelectButtonAnimation�Ŏg�p
    public void SE11()
    {
        se.volume = 0.4f;
        se.PlayOneShot(se11);
    }

    //SE�̃��Z�b�g
    public void ResetSE()
    {
        se.Stop();
        se.pitch = 1f;
        se.volume = 0.1f;
    }
}
