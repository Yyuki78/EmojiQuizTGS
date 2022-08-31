using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainGameController : MonoBehaviourPunCallbacks
{
    public enum MainGameMode
    {
        InformRole,
        QuestionTime,
        ReportAnswer,
    }

    public static MainGameMode mainmode;//���݂̃��[�h

    private Player[] Players;//�Q���҂�Player���

    public int ParticipantsNum = 0;//�Q���l��

    public int QuesitionNum = 1;//��萔

    private bool Questioner = false;
    private bool Answerer = false;

    [SerializeField] GameObject QuestionerText;
    [SerializeField] GameObject AnswererText;

    private ThemaGenerator _themaGenerator;

    [SerializeField] GameObject QuestionerPanel;
    [SerializeField] GameObject AnswererPanel;

    [SerializeField] GameObject ReportAnswerPanel;

    [SerializeField] Image myIconImage;//�����ɕ\�����ꑱ���鎩���̃A�C�R��

    [SerializeField] Image TransitionStateImage;//��ʂ��؂�ւ��ۂ̉��o�p�摜
    [SerializeField] Sprite[] TransitionImage = new Sprite[3];
    [SerializeField] Image TransitionStateBGImage;
    [SerializeField] Sprite[] TransitionBGImage = new Sprite[3];

    private bool once = true;
    private bool isSendQuestion = false;

    private PhotonView _view;

    private InformRoleDisplay _inform;

    private ReportAnswer _answer;

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Awake()
    {
        QuesitionNum = 1;

        _view = GetComponent<PhotonView>();

        _themaGenerator = GetComponent<ThemaGenerator>();

        _inform = GetComponent<InformRoleDisplay>();

        _answer = GetComponent<ReportAnswer>();

        _audio = AudioManager.GetComponent<AudioManager>();

        //�S�v���C���[�擾
        Players = PhotonNetwork.PlayerList;

        ParticipantsNum = Players.Length;

        Debug.Log("�����ʒm�X�^�[�g�I");
        OnGameStateChanged(MainGameMode.InformRole);
        /*
        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartInformRole), RpcTarget.All);
        }*/

        myIconImage.sprite = Resources.Load<Sprite>("IconImage/" + PhotonNetwork.LocalPlayer.GetScore());

        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateBGImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //MainGame�łȂ��Ȃ�Return
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        if (isSendQuestion)
        {
            _view.RPC(nameof(RpcSendMessage), RpcTarget.All, _themaGenerator._themaNum, _themaGenerator._choicesNum);
            isSendQuestion = false;
        }

    }

    // ��Ԃ��ς�����牽�����邩
    void OnGameStateChanged(MainGameMode state)
    {
        //MainGame�łȂ��Ȃ�Return
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        mainmode = state;
        switch (state)
        {
            case MainGameMode.InformRole:
                StartCoroutine(InformRole());
                break;
            case MainGameMode.QuestionTime:
                StartCoroutine(QuestionTime());
                break;
            case MainGameMode.ReportAnswer:
                StartCoroutine(ReportAnswer());
                break;
            default:
                break;
        }
    }

    //�����ʒm�@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator InformRole()
    {
        Debug.Log("�����ʒm");

        yield return new WaitForSeconds(0.01f);
        //�ϐ��̏�����
        Questioner = false;
        Answerer = false;
        QuestionerText.SetActive(false);
        AnswererText.SetActive(false);
        QuestionerPanel.SetActive(false);
        AnswererPanel.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        //�I���������������Z�b�g
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);
        ReportAnswerPanel.SetActive(false);

        yield return new WaitForSeconds(0.01f);

        //�o��ҁE�𓚎҂��萔���猈��
        if (PhotonNetwork.LocalPlayer == Players[(QuesitionNum - 1) % ParticipantsNum])
        {
            //�o���
            Questioner = true;
            _audio.SE2();
        }
        else
        {
            //�𓚎�
            Answerer = true;
            _audio.SE3();
        }
        yield return new WaitForSeconds(0.1f);

        //�����ɉ�������ʕ\��(�A�C�R���̈ʒu�̐ݒ�)
        /*if (Questioner)
        {
            QuestionerText.SetActive(true);
        }
        if (Answerer)
        {
            AnswererText.SetActive(true);
        }*/
        StartCoroutine(_inform.showIcon(Players[(QuesitionNum - 1) % ParticipantsNum]));
        yield return new WaitForSeconds(0.1f);

        //�}�X�^�[�͖��̐ݒ�
        if (PhotonNetwork.IsMasterClient)
        {
            _themaGenerator.ThemaGenerate();
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(_themaGenerator.ChoicesGenerate());
        }
        
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.5f);
        if (QuesitionNum == 1)
        {
            yield return new WaitForSeconds(1f);
        }

        //�J�E���g�_�E���\��
        yield return new WaitForSeconds(1f);

        if (once)
        {
            once = false;
            _answer.SetPosition(ParticipantsNum);
        }

        yield return new WaitForSeconds(1f);

        //�}�X�^�[�͖���z�z
        if (PhotonNetwork.IsMasterClient)
        {
            isSendQuestion = true;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(TransitionEffect4());
        yield return new WaitForSeconds(1f);

        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartQuestionTime), RpcTarget.All);
        }
    }

    //�Q�[�����@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator QuestionTime()
    {
        yield return new WaitForSeconds(0.01f);
        //�I�����\��
        if (Questioner)
        {
            QuestionerPanel.SetActive(true);
        }
        if (Answerer)
        {
            AnswererPanel.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);

        //�J�E���g�_�E���\��
        _audio.BGM3();

        yield return new WaitForSeconds(14.5f);
        //yield return new WaitForSeconds(3f);

        StartCoroutine(TransitionEffect4());
        yield return new WaitForSeconds(1f);

        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartReportAnswer), RpcTarget.All);
        }
    }

    //���U���g�@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator ReportAnswer()
    {
        ReportAnswerPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        //�v���C���[�̉𓚕\��
        //�����̕\��
        _answer.ShareAnswer(QuesitionNum, Players[(QuesitionNum - 1) % ParticipantsNum]);

        yield return new WaitForSeconds(5.5f);
        //��萔�̑���
        QuesitionNum++;
        yield return new WaitForSeconds(2.5f);

        //�}�X�^�[�͖�萔��6�Ȃ烊�U���g�֍s������
        if (PhotonNetwork.IsMasterClient && QuesitionNum == 6)
        {
            yield return new WaitForSeconds(0.5f);
            _view.RPC(nameof(StartResult), RpcTarget.All);
        }
        yield return new WaitForSeconds(0.1f);

        if (QuesitionNum == 6) yield break;

        StartCoroutine(TransitionEffect4());
        yield return new WaitForSeconds(1f);

        //���̖���
        if (PhotonNetwork.IsMasterClient)
        {
            if (QuesitionNum == 6) yield break;
            _view.RPC(nameof(StartInformRole), RpcTarget.All);
        }
    }

    //�}�X�^�[�ɂ���Ď��s�����@�X�e�[�g�̕ω�
    [PunRPC]
    private void StartQuestionTime()
    {
        Debug.Log("�^���E�𓚎��ԃX�^�[�g�I");
        OnGameStateChanged(MainGameMode.QuestionTime);
    }

    [PunRPC]
    private void StartReportAnswer()
    {
        Debug.Log("�������킹�X�^�[�g�I");
        OnGameStateChanged(MainGameMode.ReportAnswer);
    }

    [PunRPC]
    private void StartInformRole()
    {
        Debug.Log("�����ʒm�X�^�[�g�I");
        OnGameStateChanged(MainGameMode.InformRole);
    }

    //����E�I�����̔z�z
    [PunRPC]
    private void RpcSendMessage(int thema, int[] choices)
    {
        Debug.Log("����ƑI�������󂯎���Ă��ꂼ��\���I");
        _themaGenerator.Showchoices(thema, choices);
    }

    private void distributionChoice(int thema, int[] choices)
    {
        StartCoroutine(Showchoice(thema, choices));
    }

    private IEnumerator Showchoice(int thema, int[] choices)
    {
        Debug.Log("����ƑI�������󂯎���Ă��ꂼ��\���I");
        Debug.Log(thema + "," + choices[0] + "," + choices[1] + "," + choices[2] + "," + choices[3] + "," + choices[4]);
        yield return new WaitForSeconds(0.1f);
        _themaGenerator.Showchoices(thema, choices);
        yield break;
    }

    //���U���g�ֈڍs
    [PunRPC]
    private void StartResult()
    {
        Debug.Log("�Q�[���I���I�I�I");
        DebugGameManager.Instance.GoResult();
    }

    //��ʑJ�ډ��o
    private int num = 0;
    private IEnumerator TransitionEffect()
    {
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(true);
        for (int i = 0; i < 50; i++)
        {
            TransitionStateImage.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateImage.sprite = TransitionImage[num];
        num++;
        if (num == 3) num = 0;
        yield break;
    }

    //��ʑJ�ډ��o2
    private IEnumerator TransitionEffect2()
    {
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(true);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount += 0.04f;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.3f);
        TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount -= 0.04f;
            yield return new WaitForSeconds(0.015f);
        }
        TransitionStateImage.fillAmount = 0;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        //TransitionStateImage.sprite = TransitionImage[num];
        num++;
        if (num == 3) num = 0;
        yield break;
    }

    //��ʑJ�ډ��o3
    private IEnumerator TransitionEffect3()
    {
        num++;
        if (num == 3) num = 0;

        /*TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        TransitionStateImage.fillAmount = 1f;
        TransitionStateImage.color = new Color(1, 1, 1, 0);*/
        TransitionStateImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        /*TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        TransitionStateBGImage.fillAmount = 1f;
        TransitionStateBGImage.color = new Color(1, 1, 1, 0);*/
        TransitionStateBGImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);

        TransitionStateImage.sprite = TransitionImage[num];
        TransitionStateBGImage.sprite = TransitionBGImage[num];
        /*
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
            TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
            yield return new WaitForSeconds(0.02f);
        }*/
        TransitionStateImage.fillAmount = 0;
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateImage.color = new Color(1, 1, 1, 1);
        TransitionStateBGImage.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.fillAmount += 0.04f;
            TransitionStateBGImage.fillAmount += 0.04f;
            yield return new WaitForSeconds(0.02f);
        }
        TransitionStateImage.fillAmount = 1;
        TransitionStateBGImage.fillAmount = 1;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 25; i++)
        {
            TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
            TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
            yield return new WaitForSeconds(0.015f);
        }
        TransitionStateImage.color = new Color(1, 1, 1, 0);
        TransitionStateBGImage.color = new Color(1, 1, 1, 0);
        TransitionStateBGImage.fillAmount = 1;
        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.fillAmount = 0;
        TransitionStateBGImage.gameObject.SetActive(false);

        yield break;
    }

    //��ʑJ�ډ��o4
    private IEnumerator TransitionEffect4()
    {
        num++;
        if (num == 3) num = 0;

        switch (num)
        {
            case 0:
                TransitionStateImage.color = new Color(1, 1, 1, 0);
                TransitionStateBGImage.color = new Color(1, 1, 1, 0);
                TransitionStateImage.fillAmount = 1f;
                TransitionStateBGImage.fillAmount = 1f;
                break;
            case 1:
                TransitionStateImage.color = new Color(1, 1, 1, 0);
                TransitionStateBGImage.color = new Color(1, 1, 1, 0);
                TransitionStateImage.fillAmount = 1f;
                TransitionStateBGImage.fillAmount = 1f;
                TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 2:
                TransitionStateImage.color = new Color(1, 1, 1, 1);
                TransitionStateBGImage.color = new Color(1, 1, 1, 1);
                TransitionStateImage.fillAmount = 0;
                TransitionStateBGImage.fillAmount = 0;
                TransitionStateImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                TransitionStateBGImage.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("��ʑJ�ډ��o�~�X");
                break;
        }
        yield return new WaitForSeconds(0.01f);

        TransitionStateImage.sprite = TransitionImage[num];
        TransitionStateBGImage.sprite = TransitionBGImage[num];

        TransitionStateImage.gameObject.SetActive(true);
        TransitionStateBGImage.gameObject.SetActive(true);

        switch (num)
        {
            case 0:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.015f);
                }
                break;
            case 1:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color += new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color += new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.fillAmount -= 0.04f;
                    TransitionStateBGImage.fillAmount -= 0.04f;
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            case 2:
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.fillAmount += 0.04f;
                    TransitionStateBGImage.fillAmount += 0.04f;
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.3f);
                for (int i = 0; i < 25; i++)
                {
                    TransitionStateImage.color -= new Color(0, 0, 0, 0.04f);
                    TransitionStateBGImage.color -= new Color(0, 0, 0, 0.04f);
                    yield return new WaitForSeconds(0.015f);
                }
                break;
            default:
                Debug.Log("��ʑJ�ډ��o�~�X");
                break;
        }

        TransitionStateImage.gameObject.SetActive(false);
        TransitionStateBGImage.gameObject.SetActive(false);

        yield break;
    }
}
