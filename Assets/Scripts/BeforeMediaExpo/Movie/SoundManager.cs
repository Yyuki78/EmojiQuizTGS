using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource a;//AudioSource�^�̕ϐ�a��錾 �g�p����AudioSource�R���|�[�l���g���A�^�b�`�K�v
    [SerializeField] private AudioSource b;//AudioSource�^�̕ϐ�b��錾 �g�p����AudioSource�R���|�[�l���g���A�^�b�`�K�v

    [SerializeField] private AudioClip bgm1;//AudioClip�^�̕ϐ�b1��錾 �g�p����AudioClip���A�^�b�`�K�v
    [SerializeField] private AudioClip bgm2;//AudioClip�^�̕ϐ�b2��錾 �g�p����AudioClip���A�^�b�`�K�v 
    [SerializeField] private AudioClip bgm3;//AudioClip�^�̕ϐ�b3��錾 �g�p����AudioClip���A�^�b�`�K�v 

    [SerializeField] private AudioClip se1;
    [SerializeField] private AudioClip se2;
    [SerializeField] private AudioClip se3;
    [SerializeField] private AudioClip se4;
    [SerializeField] private AudioClip se5;

    //����̊֐�1 GameManger+VideoManager�Ŏg�p
    public void BGM1()
    {
        a.PlayOneShot(bgm1);
    }

    //A�ŗ����Ă��鉹�y���~�߂� GameManger�Ŏg�p
    public void StopA()
    {
        a.Stop();
    }

    //����̊֐�2 MainGameManager2�Ŏg�p
    public void BGM2()
    {
        b.PlayOneShot(bgm2);
    }

    //����̊֐�3 ResultManager�Ŏg�p
    public void BGM3()
    {
        a.PlayOneShot(bgm3);
    }

    //�N���b�N
    public void SE1()
    {
        a.PlayOneShot(se1);
    }

    //�o��� MainGameManager2�Ŏg�p
    public void SE2()
    {
        a.PlayOneShot(se2);
    }

    //�𓚎� MainGameManager2�Ŏg�p
    public void SE3()
    {
        a.PlayOneShot(se3);
    }

    //���� ShareAnswer
    public void SE4()
    {
        b.PlayOneShot(se4);
    }

    //�s���� ShareAnswer
    public void SE5()
    {
        b.PlayOneShot(se5);
    }
}
