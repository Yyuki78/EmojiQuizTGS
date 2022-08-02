using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    //���C���Q�[���Ŏg�p�����X�N���v�g�S�Ă̏�����(Start��Init���ɃR�s�y���ČĂ�)
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
        //�S�Ă��Q�[���J�n��Ԃɂ���
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
