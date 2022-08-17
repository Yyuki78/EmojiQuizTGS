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

    //DebugGameManger+VideoManager�Ŏg�p
    public void BGM1()
    {
        bgm.volume = 0.2f;
        bgm.PlayOneShot(bgm1);
    }

    //DebugGameManger�Ŏg�p
    public void BGM2()
    {
        bgm.volume = 0.05f;
        bgm.PlayOneShot(bgm2);
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
}
