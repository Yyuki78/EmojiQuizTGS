using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class ChoicesButton : MonoBehaviour
{
    //�I�����̃{�^���������ƁA�����̉𓚂��ۑ������

    [SerializeField] GameObject MainGamePanel;
    ThemaGenerator _themaGenerator;

    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    //�I�����ꂽ�摜�ԍ�
    private int choiceNum;
    private int x;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
    }

    //�{�^���ɂ��̊֐������蓖�ĂĎg�p
    public void SendChoice()
    {
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("�I�����ꂽ�̂�" + button_ob.name);
        x = int.Parse(button_ob.name) - 1;
        choiceNum = _themaGenerator._choicesNum[x];
        Debug.Log("�摜�ԍ���" + choiceNum);
        PhotonNetwork.LocalPlayer.SetChoiceNum(choiceNum);
    }
}
