using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    //リザルトの一連の動作を管理するスクリプト

    private enum ResultState{
        Loading,
        ShowResult,
        Finish
    }

    [SerializeField] GameObject MoviePanel;//Result時に非表示されるもの(後から増える)
    [SerializeField] GameObject ResultPanel;//Resultが表示されるPanel
    [SerializeField] GameObject ChangeImage;//Loadに使用する
    ChangeImage _changeImage;

    [SerializeField] GameObject SoundManager;
    SoundManager _sound;

    //スコアを表示する
    [SerializeField] GameObject Score1;
    [SerializeField] GameObject Score2;
    [SerializeField] GameObject Score3;
    [SerializeField] GameObject Score4;
    [SerializeField] GameObject Score5;
    private ScoreManager _score1;
    private ScoreManager _score2;
    private ScoreManager _score3;
    private ScoreManager _score4;
    private ScoreManager _score5;

    private bool[] Sample1 = new bool[10] { true, false, true, false, true, false, true, false, true, false };
    private bool[] Sample2 = new bool[10] { true, true, true, true, true, true, true, true, true, true };
    private bool[] Sample3 = new bool[10] { false, false, false, false, false, false, false, false, false, false };
    private bool[] Sample4 = new bool[10] { true, false, true, true, false, true, true, false, true, true };
    private bool[] Sample5 = new bool[10] { false, false, false, false, false, false, true, true, false, true };

    ShowIcon _showIcon;

    private bool once = true;//コルーチンを一度だけ起動するためのbool
    
    // Start is called before the first frame update
    private void Awake()
    {
        _changeImage = ChangeImage.GetComponent<ChangeImage>();

        _score1 = Score1.GetComponent<ScoreManager>();
        _score2 = Score2.GetComponent<ScoreManager>();
        _score3 = Score3.GetComponent<ScoreManager>();
        _score4 = Score4.GetComponent<ScoreManager>();
        _score5 = Score5.GetComponent<ScoreManager>();

        _showIcon = GetComponent<ShowIcon>();

        _sound = SoundManager.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        //ResultStateの時のみ起動
        if(GameManager.Instance.GetCurrentState() == GameManager.GameMode.Result)
        {
            if (once == true)
            {
                once = false;
                StartCoroutine("Load");
            }
        }
    }

    private IEnumerator Load()
    {
        _changeImage.Init();
        // 指定秒間待つ
        yield return new WaitForSeconds(0.65f);
        
        //ここで画面を切り替える
        MoviePanel.SetActive(false);
        ResultPanel.SetActive(true);

        StartCoroutine(_showIcon.showIcon());
        _sound.BGM3();
        StartCoroutine("ShowResult");
        yield break;
    }

    private IEnumerator ShowResult()
    {
        Debug.Log("ShowResult");
        yield return new WaitForSeconds(2.5f);
        _score1.show(ShareAnswer.answer1);
        _score2.show(ShareAnswer.answer2);
        _score3.show(ShareAnswer.answer3);
        _score4.show(ShareAnswer.answer4);
        _score5.show(ShareAnswer.answer5);
    }
}
