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

    [SerializeField] Image[] IconFlameImage = new Image[4];//�A�C�R���t���[���摜
    [SerializeField] Sprite myIconFlame;//�����̃A�C�R���t���[��
    [SerializeField] Sprite otherIconFlame;//���l�̃A�C�R���t���[��

    [SerializeField] Image[] IconImage = new Image[4];//Icon�摜��\��������Image�I�u�W�F�N�g

    [SerializeField] Image[] AnswerImage = new Image[4];//Emoji�摜��\��������Image�I�u�W�F�N�g

    //�����ƕs�����̉摜
    [SerializeField] Image[] Correct = new Image[4];

    [SerializeField] Image[] inCorrect = new Image[4];

    //�I�����Ɋւ�����̂�Z�߂��I�u�W�F�N�g
    [SerializeField] GameObject[] SelectObjects = new GameObject[4];

    //�S�̂̐���s����������bool[�������s������]
    public static bool[] answer1 = new bool[5];
    public static bool[] answer2 = new bool[5];
    public static bool[] answer3 = new bool[5];
    public static bool[] answer4 = new bool[5];
    public static bool[] answer5 = new bool[5];

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.1f);
        _themaGenerator = GetComponent<ThemaGenerator>();

        _audio = AudioManager.GetComponent<AudioManager>();

        //�e�탊�Z�b�g
        for (int j = 0; j < 4; j++)
        {
            yield return new WaitForSeconds(0.1f);
            BGImage[j].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            IconFlameImage[j].sprite = otherIconFlame;
            IconFlameImage[j].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            IconImage[j].sprite = null;
            IconImage[j].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            AnswerImage[j].sprite = Resources.Load<Sprite>("Image/129");
            AnswerImage[j].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            Correct[j].gameObject.SetActive(false);
            inCorrect[j].gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            SelectObjects[j].SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);
        CorrectImage.sprite = null;
        CorrectImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }

    public void SetPosition(int ParticipantNum)
    {
        switch (ParticipantNum)
        {
            case 2:
                SelectObjects[0].transform.localPosition = new Vector3(335, 0, 0);
                break;
            case 3:
                SelectObjects[0].transform.localPosition = new Vector3(110, 0, 0);
                SelectObjects[1].transform.localPosition = new Vector3(330, 0, 0);
                break;
            case 4:
                SelectObjects[0].transform.localPosition = new Vector3(50, 0, 0);
                SelectObjects[1].transform.localPosition = new Vector3(125, 0, 0);
                SelectObjects[2].transform.localPosition = new Vector3(200, 0, 0);
                break;
            case 5:
                SelectObjects[0].transform.localPosition = new Vector3(0, 0, 0);
                SelectObjects[1].transform.localPosition = new Vector3(0, 0, 0);
                SelectObjects[2].transform.localPosition = new Vector3(0, 0, 0);
                SelectObjects[3].transform.localPosition = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("�Q���҂̐����ُ�ł�");
                break;
        }
    }

    //�������킹
    public void ShareAnswer(int QuesitionNum, Player player)
    {
        StartCoroutine(ShowAnswer1(QuesitionNum, player));
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
            IconImage[i].sprite = Resources.Load<Sprite>("IconImage/" + player.GetScore());
            if (player == PhotonNetwork.LocalPlayer)
            {
                IconFlameImage[i].sprite = myIconFlame;
            }
            i++;
        }

        for (int j = 0; j < players.Length-1; j++)
        {
            BGImage[j].gameObject.SetActive(true);
            IconFlameImage[j].gameObject.SetActive(true);
            IconImage[j].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.01f);

        //���ꂼ��̉𓚂��Z�b�g
        int k = 0;
        foreach (var player in players)
        {
            if (player == Qplayer) continue;
            AnswerImage[k].sprite = Resources.Load<Sprite>("Image/" + player.GetChoiceNum());
            Debug.Log(player.GetChoiceNum());
            k++;
        }
        yield return new WaitForSeconds(0.01f);
        for (int j = 0; j < players.Length-1; j++)
        {
            AnswerImage[j].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);

        //�h�������[��
        yield return new WaitForSeconds(0.3f);
        _audio.SE6();
        yield return new WaitForSeconds(2.7f);

        //�������o��
        CorrectImage.sprite = Resources.Load<Sprite>("Image/" + _themaGenerator._themaNum);
        CorrectImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        //�����̐���s����
        if(PhotonNetwork.LocalPlayer.GetChoiceNum() == _themaGenerator._themaNum)
        {
            if (PhotonNetwork.LocalPlayer != Qplayer)
            {
                _audio.SE4();
            }
        }
        else
        {
            if (PhotonNetwork.LocalPlayer != Qplayer)
            {
                _audio.SE5();
            }
        }
        yield return new WaitForSeconds(0.01f);

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
            IconFlameImage[j].sprite = otherIconFlame;
            IconFlameImage[j].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

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

        yield break;
    }
}
