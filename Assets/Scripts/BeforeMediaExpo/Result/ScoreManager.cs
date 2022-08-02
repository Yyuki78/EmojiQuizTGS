using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //配列を受け取ってスコアを表示するスクリプト

    [SerializeField] Sprite correctImage;//正解画像
    [SerializeField] Sprite incorrectImage;//不正解画像

    //scoreImage1～10のオブジェクト
    [SerializeField] private Image ScoreImage1;
    [SerializeField] private Image ScoreImage2;
    [SerializeField] private Image ScoreImage3;
    [SerializeField] private Image ScoreImage4;
    [SerializeField] private Image ScoreImage5;
    [SerializeField] private Image ScoreImage6;
    [SerializeField] private Image ScoreImage7;
    [SerializeField] private Image ScoreImage8;
    [SerializeField] private Image ScoreImage9;
    [SerializeField] private Image ScoreImage10;
    private Image[] scoreImage;

    // Start is called before the first frame update
    void Awake()
    {
        scoreImage = new Image[]
        {
            ScoreImage1,ScoreImage2,ScoreImage3,ScoreImage4,ScoreImage5,ScoreImage6,ScoreImage7,ScoreImage8,ScoreImage9,ScoreImage10
        };
    }

    public void show(bool[] score)
    {
        StartCoroutine(Show(score));
    }

    //一個ずつスコアを表示していくコルーチン
    private IEnumerator Show(bool[] score)//ここで受け取るのは10個のbool
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("スコアの表示を始めます");

        for(int i = 0; i < 10; i++)
        {
            //ここで1つずつ正解不正解を表示する
            if (score[i] == true)
            {
                //正解画像をi番目の画像に入れる
                scoreImage[i].sprite = correctImage;
            }
            else
            {
                //不正解画像をi番目の画像に入れる
                scoreImage[i].sprite = incorrectImage;
            }
            yield return new WaitForSeconds(0.75f);
        }
        yield break;
    }
}
