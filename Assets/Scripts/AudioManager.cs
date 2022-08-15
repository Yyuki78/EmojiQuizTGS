using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    [SerializeField] private AudioClip bgm1;//ムービー
    [SerializeField] private AudioClip bgm2;//部屋内
    [SerializeField] private AudioClip bgm3;//ゲーム中
    [SerializeField] private AudioClip bgm4;//リザルト

    [SerializeField] private AudioClip se1;//
    [SerializeField] private AudioClip se2;//
    [SerializeField] private AudioClip se3;//
    [SerializeField] private AudioClip se4;//
    [SerializeField] private AudioClip se5;//

    //自作の関数1 GameManger+VideoManagerで使用
    public void BGM1()
    {
        bgm.PlayOneShot(bgm1);
    }

    //Aで流している音楽を止める GameMangerで使用
    public void StopA()
    {
        bgm.Stop();
    }

    //自作の関数2 MainGameManager2で使用
    public void BGM2()
    {
        bgm.PlayOneShot(bgm2);
    }

    //自作の関数3 ResultManagerで使用
    public void BGM3()
    {
        bgm.PlayOneShot(bgm3);
    }

    //クリック
    public void SE1()
    {
        se.PlayOneShot(se1);
    }

    //出題者 MainGameManager2で使用
    public void SE2()
    {
        se.PlayOneShot(se2);
    }

    //解答者 MainGameManager2で使用
    public void SE3()
    {
        se.PlayOneShot(se3);
    }

    //正解 ShareAnswer
    public void SE4()
    {
        se.PlayOneShot(se4);
    }

    //不正解 ShareAnswer
    public void SE5()
    {
        se.PlayOneShot(se5);
    }
}
