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
    [SerializeField] private AudioClip se8;//バーが伸びる
    [SerializeField] private AudioClip se9;//画面遷移演出1

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

    //バーが伸びる ReportAnswerで使用
    public void SE8()
    {
        se.volume = 0.2f;
        se.PlayOneShot(se8);
    }

    //画面遷移演出1 ReportAnswerで使用
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

    //タイトルに戻る際のカウントダウン演出
    public void SE10()
    {
        se.pitch = 0.2f;
        se.volume = 0.2f;
        //se.PlayOneShot(se8);
    }

    //SEのリセット
    public void ResetSE()
    {
        se.Stop();
        se.pitch = 1f;
        se.volume = 0.1f;
    }
}
