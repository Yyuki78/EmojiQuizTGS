using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField] Image[] IconImage = new Image[5];//Icon�摜��\��������Image�I�u�W�F�N�g

    private ResultStaging[] _staging = new ResultStaging[5];//�o�[���o

    [SerializeField] GameObject[] BarPos = new GameObject[5];//���ꂼ��̃o�[�E�A�C�R���̍���(�Q���҂̐��ŕς��)

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _staging = GetComponentsInChildren<ResultStaging>();
        for(int i = 0; i < 5; i++)
        {
            IconImage[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() == DebugGameManager.GameMode.Result)
        {
            if (once)
            {
                StartCoroutine(Result());
                once = false;
            }
        }
    }

    private IEnumerator Result()
    {
        var players = PhotonNetwork.PlayerList;
        yield return new WaitForSeconds(0.01f);

        switch (players.Length)
        {
            case 2:
                BarPos[0].transform.localPosition = new Vector3(0, -75, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -325, 0);
                break;
            case 3:
                BarPos[0].transform.localPosition = new Vector3(0, 0, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -400, 0);
                break;
            case 4:
                BarPos[0].transform.localPosition = new Vector3(0, 50, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -125, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -300, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -475, 0);
                break;
            case 5:
                BarPos[0].transform.localPosition = new Vector3(0, 100, 0);
                BarPos[1].transform.localPosition = new Vector3(0, -50, 0);
                BarPos[2].transform.localPosition = new Vector3(0, -200, 0);
                BarPos[3].transform.localPosition = new Vector3(0, -350, 0);
                BarPos[4].transform.localPosition = new Vector3(0, -500, 0);
                break;
            default:
                Debug.Log("�Q���҂̐������������ł�");
                break;
        }


        yield return new WaitForSeconds(0.01f);

        //�A�C�R���̃Z�b�g
        int i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            IconImage[i].sprite = Resources.Load<Sprite>("IconImage/" + player.GetScore());
            IconImage[i].gameObject.SetActive(true);
            i++;
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("ShowResult");
        StartCoroutine(_staging[0].Staging(ReportAnswer.answer1, 1));
        StartCoroutine(_staging[1].Staging(ReportAnswer.answer2, 2));
        if (players.Length > 2)
        {
            StartCoroutine(_staging[2].Staging(ReportAnswer.answer3, 3));
        }
        if (players.Length > 3)
        {
            StartCoroutine(_staging[3].Staging(ReportAnswer.answer4, 4));
        }
        if (players.Length > 4)
        {
            StartCoroutine(_staging[4].Staging(ReportAnswer.answer5, 5));
        }

        yield return new WaitForSeconds(15f);

        //�J�E���g�_�E��
        yield return new WaitForSeconds(5f);


        //��������ޏo���A�V�[���������[�h����
        Debug.Log("�^�C�g���ɖ߂�܂�");

        //�Ó]�Ȃǂ̉��o�����Ă�����

        //�������������Z�b�g
        PhotonNetwork.LocalPlayer.SetReady(false);
        yield return new WaitForSeconds(0.01f);

        //�A�C�R�������Z�b�g
        PhotonNetwork.LocalPlayer.SetScore(0);
        yield return new WaitForSeconds(0.01f);

        //�I���������������Z�b�g
        PhotonNetwork.LocalPlayer.SetChoiceNum(129);
        yield return new WaitForSeconds(0.01f);

        // ���[������ޏo����
        PhotonNetwork.LeaveRoom();

        yield return new WaitForSeconds(0.01f);
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene("MainScene");
    }
}
