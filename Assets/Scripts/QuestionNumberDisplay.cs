using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionNumberDisplay : MonoBehaviour
{
    //既に終わった問題数の画像
    [SerializeField] Image[] QuestionImage = new Image[5];

    //既に終わった問題数の画像
    [SerializeField] Sprite FillQuestionImage;
    //まだ終わっていない問題数の画像
    [SerializeField] Sprite EmptyQuestionImage;

    private MainGameController _main;

    private int quesitionNum = 0;
    private bool plus = false;

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        QuestionImage[0].sprite = FillQuestionImage;
        for (int i = 1; i < 5; i++)
        {
            QuestionImage[i].sprite = EmptyQuestionImage;
        }

        _main = GetComponentInParent<MainGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        if (MainGameController.mainmode == MainGameController.MainGameMode.InformRole)
        {
            if (once)
            {
                quesitionNum = _main.QuesitionNum;
                switch (quesitionNum)
                {
                    case 1:
                        break;
                    case 2:
                        QuestionImage[0].color = new Color(1, 1, 1, 1);
                        QuestionImage[1].sprite = FillQuestionImage;
                        break;
                    case 3:
                        QuestionImage[1].color = new Color(1, 1, 1, 1);
                        QuestionImage[1].sprite = FillQuestionImage;
                        QuestionImage[2].sprite = FillQuestionImage;
                        break;
                    case 4:
                        QuestionImage[2].color = new Color(1, 1, 1, 1);
                        QuestionImage[1].sprite = FillQuestionImage;
                        QuestionImage[2].sprite = FillQuestionImage;
                        QuestionImage[3].sprite = FillQuestionImage;
                        break;
                    case 5:
                        QuestionImage[3].color = new Color(1, 1, 1, 1);
                        QuestionImage[1].sprite = FillQuestionImage;
                        QuestionImage[2].sprite = FillQuestionImage;
                        QuestionImage[3].sprite = FillQuestionImage;
                        QuestionImage[4].sprite = FillQuestionImage;
                        break;
                    default:
                        Debug.Log("問題数の設定ミス");
                        break;
                }
                once = false;
            }

            if (QuestionImage[quesitionNum - 1].color.a >= 1)
            {
                plus = false;
            }
            if (QuestionImage[quesitionNum - 1].color.a <= 0.5f)
            {
                plus = true;
            }
            if (!plus)
            {
                QuestionImage[quesitionNum - 1].color -= new Color(0, 0, 0, 0.01f);
            }
            else
            {
                QuestionImage[quesitionNum - 1].color += new Color(0, 0, 0, 0.01f);
            }
        }else if(MainGameController.mainmode == MainGameController.MainGameMode.ReportAnswer)
        {
            once = true;
        }
    }
}
