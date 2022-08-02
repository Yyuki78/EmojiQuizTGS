using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    //メインゲームで使用したスクリプト全ての初期化(StartをInit等にコピペして呼ぶ)
    MainGameManager2 _mainGameManager;
    ChooseThema _chooseThema;
    Timer _timer;
    QuestionsNumber _questionNumber;
    [SerializeField] GameObject CorrectAnswersPanel;
    ShareAnswer _shareAnswer;
    [SerializeField] GameObject LoadingPanel;
    LoadQuestionNumber _loadQuestionNumber;
    // Start is called before the first frame update
    void Awake()
    {
        _mainGameManager = GetComponent<MainGameManager2>();
        _chooseThema = GetComponent<ChooseThema>();
        _timer = GetComponent<Timer>();
        _questionNumber = GetComponent<QuestionsNumber>();
        _shareAnswer = CorrectAnswersPanel.GetComponent<ShareAnswer>();
        _loadQuestionNumber = LoadingPanel.GetComponent<LoadQuestionNumber>();
    }

    public void ResetAll()
    {
        //全てをゲーム開始状態にする
        StartCoroutine("Reset");
    }

    private IEnumerator Reset()
    {
        _mainGameManager.Init();
        yield return new WaitForSeconds(0.1f);
        _chooseThema.Init();
        yield return new WaitForSeconds(0.1f);
        _timer.Init();
        yield return new WaitForSeconds(0.1f);
        _questionNumber.Init();
        yield return new WaitForSeconds(0.1f);
        _shareAnswer.Init();
        yield return new WaitForSeconds(0.2f);
        _loadQuestionNumber.Init();
    }
}
