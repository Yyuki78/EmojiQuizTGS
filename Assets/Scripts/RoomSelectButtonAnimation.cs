using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RoomSelectButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int ButtonMode = 0; //inspector�ŕύX����

    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspector�Őݒ肷��

    private bool isDirect = false;//�N���b�N���Ɉ�x�J�[�\�����d�Ȃ镔�����o�R�������ǂ���

    [SerializeField] GameObject[] DoorMassObj = new GameObject[2];

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // �{�^�����������
        switch (ButtonMode)
        {
            case 0:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // �{�^�����������
        switch (ButtonMode)
        {
            case 0:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // �{�^����������A���̌�h���b�O���삪���邱�ƂȂ��{�^�����������
        switch (ButtonMode)
        {
            case 0:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                _audio.SE11();
                this.GetComponent<RoomSelectButtonAnimation>().enabled = false;
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                _audio.SE11();
                this.GetComponent<RoomSelectButtonAnimation>().enabled = false;
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //�{�^���͈̔͂Ƀ}�E�X�J�[�\��������
        switch (ButtonMode)
        {
            case 0:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //�{�^���͈̔͂���}�E�X�J�[�\�����o��
        switch (ButtonMode)
        {
            case 0:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }
}
