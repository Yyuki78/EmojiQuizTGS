using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource a;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要
    [SerializeField] private AudioSource b;//AudioSource型の変数bを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    [SerializeField] private AudioClip bgm1;//AudioClip型の変数b1を宣言 使用するAudioClipをアタッチ必要
    [SerializeField] private AudioClip bgm2;//AudioClip型の変数b2を宣言 使用するAudioClipをアタッチ必要 
    [SerializeField] private AudioClip bgm3;//AudioClip型の変数b3を宣言 使用するAudioClipをアタッチ必要 

    [SerializeField] private AudioClip se1;
    [SerializeField] private AudioClip se2;
    [SerializeField] private AudioClip se3;
    [SerializeField] private AudioClip se4;
    [SerializeField] private AudioClip se5;

    //自作の関数1 GameManger+VideoManagerで使用
    public void BGM1()
    {
        a.PlayOneShot(bgm1);
    }

    //Aで流している音楽を止める GameMangerで使用
    public void StopA()
    {
        a.Stop();
    }

    //自作の関数2 MainGameManager2で使用
    public void BGM2()
    {
        b.PlayOneShot(bgm2);
    }

    //自作の関数3 ResultManagerで使用
    public void BGM3()
    {
        a.PlayOneShot(bgm3);
    }

    //クリック
    public void SE1()
    {
        a.PlayOneShot(se1);
    }

    //出題者 MainGameManager2で使用
    public void SE2()
    {
        a.PlayOneShot(se2);
    }

    //解答者 MainGameManager2で使用
    public void SE3()
    {
        a.PlayOneShot(se3);
    }

    //正解 ShareAnswer
    public void SE4()
    {
        b.PlayOneShot(se4);
    }

    //不正解 ShareAnswer
    public void SE5()
    {
        b.PlayOneShot(se5);
    }
}
