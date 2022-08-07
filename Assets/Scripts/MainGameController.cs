using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private int ParticipantsNum = 0;//�Q���l��

    private int QuesitionNum = 0;//��萔

    private bool Questioner = false;
    private bool Answerer = false;

    [SerializeField] GameObject QuestionerText;
    [SerializeField] GameObject AnswererText;

    private ThemaGenerator _themaGenerator;

    [SerializeField] GameObject QuestionerPanel;
    [SerializeField] GameObject AnswererPanel;


    private bool once = true;

    private PhotonView _view;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();

        _themaGenerator = GetComponent<ThemaGenerator>();

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
    }

    // Update is called once per frame
    void Update()
    {
        //MainGame�łȂ��Ȃ�Return
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;


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

        //��萔�̑���
        QuesitionNum++;
        yield return new WaitForSeconds(0.01f);

        //�}�X�^�[�͖��̐ݒ�y�єz�z
        if (PhotonNetwork.IsMasterClient)
        {
            _themaGenerator.ThemaGenerate();
            _themaGenerator.ChoicesGenerate();
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, _themaGenerator._themaNum, _themaGenerator._choicesNum);
        }
        yield return new WaitForSeconds(0.01f);

        //�o��ҁE�𓚎҂��萔���猈��
        if (PhotonNetwork.LocalPlayer == Players[(QuesitionNum - 1) % ParticipantsNum])
        {
            //�o���
            Questioner = true;
        }
        else
        {
            //�𓚎�
            Answerer = true;
        }
        yield return new WaitForSeconds(0.01f);

        //�����ɉ�������ʕ\��(�A�C�R���̈ʒu�̐ݒ�)
        if (Questioner)
        {
            QuestionerText.SetActive(true);
        }
        if (Answerer)
        {
            AnswererText.SetActive(true);
        }
        yield return new WaitForSeconds(0.01f);

        //�J�E���g�_�E���\��
        yield return new WaitForSeconds(3f);

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

        yield return new WaitForSeconds(11f);
        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC(nameof(StartReportAnswer), RpcTarget.All);
        }
    }

    //���U���g�@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator ReportAnswer()
    {
        yield return new WaitForSeconds(0.1f);
        //�v���C���[�̉𓚕\��

        //�����̕\��

        //���ւ̃J�E���g�_�E���\��

        //�}�X�^�[�͖�萔��5�Ȃ烊�U���g�֍s������
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
}
