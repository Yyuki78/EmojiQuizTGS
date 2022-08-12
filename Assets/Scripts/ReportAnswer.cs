using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ReportAnswer : MonoBehaviourPunCallbacks
{
    private ThemaGenerator _themaGenerator;
    
    [SerializeField] Image CorrectImage;

    [SerializeField] Image[] BGImage = new Image[4];//�w�i�摜

    [SerializeField] Image[] IconImage = new Image[4];//Icon�摜��\��������Image�I�u�W�F�N�g

    [SerializeField] Image[] AnswerImage = new Image[4];//Emoji�摜��\��������Image�I�u�W�F�N�g

    //�����ƕs�����̉摜
    [SerializeField] Image[] Correct = new Image[4];

    [SerializeField] Image[] inCorrect = new Image[4];

    //�S�̂̐���s����������bool[�������s������]
    public static bool[] answer1 = new bool[5];
    public static bool[] answer2 = new bool[5];
    public static bool[] answer3 = new bool[5];
    public static bool[] answer4 = new bool[5];
    public static bool[] answer5 = new bool[5];

    // Start is called before the first frame update
    void Start()
    {
        _themaGenerator = GetComponent<ThemaGenerator>();

        //�e�탊�Z�b�g
        for (int j = 0; j < 4; j++)
        {
            BGImage[j].gameObject.SetActive(false);

            IconImage[j].sprite = null;
            IconImage[j].gameObject.SetActive(false);

            AnswerImage[j].sprite = null;
            AnswerImage[j].gameObject.SetActive(false);

            Correct[j].gameObject.SetActive(false);
            inCorrect[j].gameObject.SetActive(false);
        }
        CorrectImage.sprite = null;
        CorrectImage.gameObject.SetActive(false);
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);
    }

    public void ShareAnswer(int ParticipantNum, int QuesitionNum, Player player)
    {
        switch (ParticipantNum)
        {
            case 2:
                StartCoroutine(ShowAnswer1(QuesitionNum, player));
                break;
            case 3:
                StartCoroutine(ShowAnswer1(QuesitionNum, player));
                break;
            case 4:
                StartCoroutine(ShowAnswer1(QuesitionNum, player));
                break;
            case 5:
                StartCoroutine(ShowAnswer1(QuesitionNum, player));
                break;
            default:
                Debug.Log("�Q���l�������������ł�");
                break;
        }
    }

    private IEnumerator ShowAnswer1(int Qnum, Player Qplayer)
    {
        var players = PhotonNetwork.PlayerList;
        yield return new WaitForSeconds(0.01f);

        //�A�C�R���̃Z�b�g
        int i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            if (player == Qplayer) continue;
            IconImage[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
            i++;
        }

        for (int j = 0; j < players.Length-1; j++)
        {
            BGImage[j].gameObject.SetActive(true);
            IconImage[j].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);

        //���ꂼ��̉𓚂��Z�b�g
        int k = 0;
        foreach (var player in players)
        {
            if (player == Qplayer) continue;
            AnswerImage[k].sprite = Resources.Load<Sprite>("Image/" + player.GetChoiceNum());
            Debug.Log(player.GetChoiceNum());
            k++;
        }
        for (int j = 0; j < players.Length-1; j++)
        {
            AnswerImage[j].gameObject.SetActive(true);
        }

        //�h�������[��
        yield return new WaitForSeconds(3.0f);

        //�������o��
        CorrectImage.sprite = Resources.Load<Sprite>("Image/" + _themaGenerator._themaNum);
        CorrectImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        //���ꂼ��̐���s�����̕\��
        i = 0;
        k = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            if (player == Qplayer) continue;
            if (player.GetChoiceNum() == _themaGenerator._themaNum)
            {
                Debug.Log("����");
                Correct[k].gameObject.SetActive(true);
                i++;
            }
            else
            {
                Debug.Log("�s����");
                inCorrect[k].gameObject.SetActive(true);
            }

            k++;
        }

        //����s�������i�[
        k = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            k++;

            //�o��҂̐���s����
            if (player == Qplayer)
            {
                if (i >= (players.Length - 1) / 2f)
                {
                    switch (k)
                    {
                        case 1:
                            answer1[Qnum - 1] = true;
                            break;
                        case 2:
                            answer2[Qnum - 1] = true;
                            break;
                        case 3:
                            answer3[Qnum - 1] = true;
                            break;
                        case 4:
                            answer4[Qnum - 1] = true;
                            break;
                        case 5:
                            answer5[Qnum - 1] = true;
                            break;
                    }
                }
                else
                {
                    switch (k)
                    {
                        case 1:
                            answer1[Qnum - 1] = false;
                            break;
                        case 2:
                            answer2[Qnum - 1] = false;
                            break;
                        case 3:
                            answer3[Qnum - 1] = false;
                            break;
                        case 4:
                            answer4[Qnum - 1] = false;
                            break;
                        case 5:
                            answer5[Qnum - 1] = false;
                            break;
                    }
                }
                continue;
            }

            //�𓚎҂̐���s����
            if (player.GetChoiceNum() == _themaGenerator._themaNum)
            {
                switch (k)
                {
                    case 1:
                        answer1[Qnum - 1] = true;
                        break;
                    case 2:
                        answer2[Qnum - 1] = true;
                        break;
                    case 3:
                        answer3[Qnum - 1] = true;
                        break;
                    case 4:
                        answer4[Qnum - 1] = true;
                        break;
                    case 5:
                        answer5[Qnum - 1] = true;
                        break;
                }
            }
            else
            {
                switch (k)
                {
                    case 1:
                        answer1[Qnum - 1] = false;
                        break;
                    case 2:
                        answer2[Qnum - 1] = false;
                        break;
                    case 3:
                        answer3[Qnum - 1] = false;
                        break;
                    case 4:
                        answer4[Qnum - 1] = false;
                        break;
                    case 5:
                        answer5[Qnum - 1] = false;
                        break;
                }
            }
        }

        yield return new WaitForSeconds(5f);
        //�e�탊�Z�b�g
        for(int j = 0; j < players.Length-1; j++)
        {
            IconImage[j].sprite = null;
            IconImage[j].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            AnswerImage[j].sprite = Resources.Load<Sprite>("Image/129");
            AnswerImage[j].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            Correct[j].gameObject.SetActive(false);
            inCorrect[j].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        CorrectImage.sprite = null;
        CorrectImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        //�I���������������Z�b�g
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);

        yield break;
    }
}
