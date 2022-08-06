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

    [SerializeField] GameObject QuestionerPanel;
    [SerializeField] GameObject AnswererPanel;


    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        //�S�v���C���[�擾
        Players = PhotonNetwork.PlayerList;

        ParticipantsNum = Players.Length;

        OnGameStateChanged(MainGameMode.InformRole);
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
                
                break;
            case MainGameMode.ReportAnswer:
                
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
        QuestionerPanel.SetActive(false);
        AnswererPanel.SetActive(false);

        yield return new WaitForSeconds(0.01f);

        //��萔�̑���
        QuesitionNum++;
        yield return new WaitForSeconds(0.01f);

        //�}�X�^�[�͖��̐ݒ�y�єz�z
        if (PhotonNetwork.IsMasterClient)
        {

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
            QuestionerPanel.SetActive(true);
        }
        if (Answerer)
        {
            AnswererPanel.SetActive(true);
        }
        yield return new WaitForSeconds(0.01f);

        //�J�E���g�_�E���\��
        yield return new WaitForSeconds(3f);

        Debug.Log("�^���E�𓚎��ԃX�^�[�g�I");
    }

    //�Q�[�����@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator QuestionTime()
    {
        yield return new WaitForSeconds(0.1f);
        //�I�����\��
        //�J�E���g�_�E���\��

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
}
