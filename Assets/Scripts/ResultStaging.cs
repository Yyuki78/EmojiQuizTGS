using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStaging : MonoBehaviour
{
    [SerializeField] GameObject[] BarImage = new GameObject[5];
    private Transform[] BarPos = new Transform[5];//正解時のバーの座標
    private Image[] CorrectImage = new Image[5];//正解時のバー画像

    public bool isFinishResult = false;

    private int xPos = -250;//伸ばすバーのx座標

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            BarPos[i] = BarImage[i].GetComponent<Transform>();
            CorrectImage[i] = BarImage[i].GetComponent<Image>();
            CorrectImage[i].fillAmount = 0;
        }
    }

    public IEnumerator Staging(bool[] score, int num)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("スコアの表示を始めます");

        for (int i = 0; i < 5; i++)
        {
            //ここで1つずつ正解不正解を表示する
            if (score[i] == true)
            {
                Debug.Log(num + "番は正解");
                BarPos[i].localPosition = new Vector2(xPos, 200);
                //正解なら問題数に合ったバーを伸ばす
                for (int j = 0; j < 100; j++)
                {
                    CorrectImage[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.01f);
                }
                xPos += 150;
            }
            else
            {
                Debug.Log(num + "番は不正解");
                if (ReportAnswer.answer1[i] == false && ReportAnswer.answer2[i] == false && ReportAnswer.answer3[i] == false && ReportAnswer.answer4[i] == false && ReportAnswer.answer5[i] == false)
                {
                    Debug.Log("全員が不正解です");
                }
                else
                {
                    for (int j = 0; j < 100; j++)
                    {
                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("リザルト終わりました");
        isFinishResult = true;

        yield break;
    }
}
