using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class ChoicesButton : MonoBehaviour
{
    //�I�����̃{�^���������ƁA�����̉𓚂��ۑ������

    //�{�^���̃��[�h(0�͘g���؂�ւ��,1�͉����ꂽ�摜�ɂȂ�)
    [SerializeField] int ButtonMode = 0;

    [SerializeField] GameObject MainGamePanel;
    ThemaGenerator _themaGenerator;

    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    //�I���������Ƃ�������g�摜
    [SerializeField] GameObject ChooseFlame;

    //�I�������E����Ă��Ȃ��{�^���摜
    [SerializeField] Sprite ButtonImage;
    [SerializeField] Sprite pushButtonImage;

    //�{�^���̉摜������ꏊ
    [SerializeField] GameObject[] Buttons = new GameObject[5];
    private Image[] buttonImages = new Image[5];
    private Transform[] choiceImages = new Transform[5];

    //�I�����ꂽ�摜�ԍ�
    private int choiceNum;
    private int x;

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
        ChooseFlame.SetActive(false);
        _audio = AudioManager.GetComponent<AudioManager>();
        for (int i = 0; i < 5; i++)
        {
            buttonImages[i] = Buttons[i].GetComponent<Image>();
            var x = Buttons[i].GetComponentsInChildrenWithoutSelf<Transform>();
            choiceImages[i] = x[0];
        }
    }

    //�{�^���ɂ��̊֐������蓖�ĂĎg�p
    public void SendChoice()
    {
        _audio.SE1();

        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("�I�����ꂽ�̂�" + button_ob.name);
        x = int.Parse(button_ob.name) - 1;
        choiceNum = _themaGenerator._choicesNum[x];
        Debug.Log("�摜�ԍ���" + choiceNum);
        PhotonNetwork.LocalPlayer.SetChoiceNum(choiceNum);

        switch (ButtonMode)
        {
            case 0:
                switch (int.Parse(button_ob.name))
                {
                    case 1:
                        ChooseFlame.transform.localPosition = new Vector3(-150, 125, 0);
                        break;
                    case 2:
                        ChooseFlame.transform.localPosition = new Vector3(150, 125, 0);
                        break;
                    case 3:
                        ChooseFlame.transform.localPosition = new Vector3(-300, -140, 0);
                        break;
                    case 4:
                        ChooseFlame.transform.localPosition = new Vector3(0, -140, 0);
                        break;
                    case 5:
                        ChooseFlame.transform.localPosition = new Vector3(300, -140, 0);
                        break;
                    default:
                        Debug.Log("�����ꂽ�{�^�����Ⴂ�܂�");
                        break;
                }
                ChooseFlame.SetActive(true);
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    buttonImages[i].sprite = ButtonImage;
                    choiceImages[i].localPosition = new Vector3(5.7f, 11f, 0);
                }

                buttonImages[int.Parse(button_ob.name) - 1].sprite = pushButtonImage;
                choiceImages[int.Parse(button_ob.name) - 1].localPosition = new Vector3(-5, 3, 0);

                break;
            default:
                Debug.Log("ButtonMode���Ⴂ�܂�");
                break;
        }
    }

    private void OnDisable()
    {
        switch (ButtonMode)
        {
            case 0:
                ChooseFlame.SetActive(false);
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    buttonImages[i].sprite = ButtonImage;
                    choiceImages[i].localPosition = new Vector3(5.7f, 11f, 0);
                }
                break;
            default:
                Debug.Log("ButtonMode���Ⴂ�܂�");
                break;
        }
    }
}
