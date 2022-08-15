using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int ButtonMode = 0; //inspector�ŕύX����

    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspector�Őݒ肷��

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
                transform.DOScale(0.95f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(0.8f, 0.24f).SetEase(Ease.OutCubic);

                _audio.SE1();
                break;
            case 1:
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
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
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
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
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
                transform.DOScale(1.1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1.05f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
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
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
                break;
            default:
                Debug.Log("�~�X");
                break;
        }
    }
}
