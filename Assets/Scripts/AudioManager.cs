using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    [SerializeField] private AudioClip bgm1;//���[�r�[
    [SerializeField] private AudioClip bgm2;//������
    [SerializeField] private AudioClip bgm3;//�Q�[����
    [SerializeField] private AudioClip bgm4;//���U���g

    [SerializeField] private AudioClip se1;//
    [SerializeField] private AudioClip se2;//
    [SerializeField] private AudioClip se3;//
    [SerializeField] private AudioClip se4;//
    [SerializeField] private AudioClip se5;//

    //����̊֐�1 GameManger+VideoManager�Ŏg�p
    public void BGM1()
    {
        bgm.PlayOneShot(bgm1);
    }

    //A�ŗ����Ă��鉹�y���~�߂� GameManger�Ŏg�p
    public void StopA()
    {
        bgm.Stop();
    }

    //����̊֐�2 MainGameManager2�Ŏg�p
    public void BGM2()
    {
        bgm.PlayOneShot(bgm2);
    }

    //����̊֐�3 ResultManager�Ŏg�p
    public void BGM3()
    {
        bgm.PlayOneShot(bgm3);
    }

    //�N���b�N
    public void SE1()
    {
        se.PlayOneShot(se1);
    }

    //�o��� MainGameManager2�Ŏg�p
    public void SE2()
    {
        se.PlayOneShot(se2);
    }

    //�𓚎� MainGameManager2�Ŏg�p
    public void SE3()
    {
        se.PlayOneShot(se3);
    }

    //���� ShareAnswer
    public void SE4()
    {
        se.PlayOneShot(se4);
    }

    //�s���� ShareAnswer
    public void SE5()
    {
        se.PlayOneShot(se5);
    }
}
