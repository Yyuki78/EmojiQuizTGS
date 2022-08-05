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


    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MainGame�łȂ��Ȃ�Return
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;


    }

    //�����ʒm�@�}�X�^�[�����RPC�Ŏ��s
    private IEnumerator InformRole()
    {
        yield return new WaitForSeconds(0.1f);
        //�ϐ��̏�����

        //��萔�̑���

        //�}�X�^�[�͖��̐ݒ�y�єz�z

        //�o��ҁE�𓚎҂��萔���猈��

        //�����ɉ�������ʕ\��

        //�J�E���g�_�E���\��
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
