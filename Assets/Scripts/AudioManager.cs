using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    [SerializeField] private AudioClip bgm1;//タイトル
    [SerializeField] private AudioClip bgm2;//部屋内
    [SerializeField] private AudioClip bgm3;//ゲーム中
    [SerializeField] private AudioClip bgm4;//リザルト

    [SerializeField] private AudioClip se1;//クリック音
    [SerializeField] private AudioClip se2;//出題者
    [SerializeField] private AudioClip se3;//解答者
    [SerializeField] private AudioClip se4;//正解
    [SerializeField] private AudioClip se5;//不正解
    [SerializeField] private AudioClip se6;//ドラムロール
    [SerializeField] private AudioClip se7;//ドアガチャガチャ

    //DebugGameManger+VideoManagerで使用
    public void BGM1()
    {
        bgm.volume = 0.2f;
        bgm.PlayOneShot(bgm1);
    }

    //DebugGameMangerで使用
    public void BGM2()
    {
        bgm.volume = 0.05f;
        bgm.PlayOneShot(bgm2);
    }

    //MainGameControllerで使用
    public void BGM3()
    {
        bgm.volume = 0.2f;
        bgm.PlayOneShot(bgm3);
    }

    //DebugGameMangerで使用
    public void BGM4()
    {
        bgm.volume = 0.15f;
        bgm.PlayOneShot(bgm4);
    }

    //全てのBGMを止める DebugGameMangerで使用
    public void StopBGM()
    {
        bgm.Stop();
    }

    //クリック InRoom,DebugGameManger,ChoicesButtonで使用
    public void SE1()
    {
        se.volume = 0.2f;
        se.PlayOneShot(se1);
    }

    //出題者 MainGameControllerで使用
    public void SE2()
    {
        se.volume = 0.1f;
        se.PlayOneShot(se2);
    }

    //解答者 MainGameControllerで使用
    public void SE3()
    {
        se.volume = 0.1f;
        se.PlayOneShot(se3);
    }

    //正解 ReportAnswerで使用
    public void SE4()
    {
        se.volume = 0.15f;
        se.PlayOneShot(se4);
    }

    //不正解 ReportAnswerで使用
    public void SE5()
    {
        se.volume = 0.15f;
        se.PlayOneShot(se5);
    }

    //ドラムロール ReportAnswerで使用
    public void SE6()
    {
        se.volume = 0.5f;
        se.PlayOneShot(se6);
    }

    //ドアガチャガチャ ReportAnswerで使用
    public void SE7()
    {
        se.volume = 0.75f;
        se.PlayOneShot(se7);
    }
}
