using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ReadyButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspector�Őݒ肷��

    [SerializeField] GameObject alreadySelectPanel;
    [SerializeField] Image checkImage;
    [SerializeField] Image okImage;
    private Image _buttonImage;

    private bool isDirect = false;//�N���b�N���Ɉ�x�J�[�\�����d�Ȃ镔�����o�R�������ǂ���

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _buttonImage = GetComponent<Image>();
        alreadySelectPanel.SetActive(false);
        okImage.gameObject.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // �{�^�����������
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // �{�^�����������
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // �{�^����������A���̌�h���b�O���삪���邱�ƂȂ��{�^�����������
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
        StartCoroutine(ClickEffect());
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //�{�^���͈̔͂Ƀ}�E�X�J�[�\��������
        transform.DOScale(1.1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1.05f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //�{�^���͈̔͂���}�E�X�J�[�\�����o��
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    private IEnumerator ClickEffect()
    {
        okImage.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.01f);
        _buttonImage.raycastTarget = false;
        alreadySelectPanel.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            checkImage.fillAmount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            if (i == 18)
            {
                okImage.gameObject.SetActive(true);
                okImage.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.InOutQuad);
            }
        }
        checkImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.08f);

        okImage.gameObject.transform.DOPunchPosition(new Vector3(1f, 0, 0), 0.15f);
        okImage.gameObject.transform.DOLocalMove(new Vector3(10f, 20f, 0), 0.15f).SetEase(Ease.InOutQuart);
        okImage.gameObject.transform.DOLocalRotate(new Vector3(0, 0, -13f), 0.15f).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(0.2f);
        /*
        for (int i = 0; i < 12; i++)
        {
            okImage.gameObject.transform.localPosition += new Vector3(0, 2.4f, 0);
            okImage.gameObject.transform.localEulerAngles -= new Vector3(0, 0, 1.5f);
            yield return new WaitForSeconds(0.01f);
        }*/

        okImage.gameObject.transform.DOPunchPosition(new Vector3(0, 0, 0), 0.15f);
        okImage.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InOutQuart);
        okImage.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(0.2f);
        /*
        for (int i = 0; i < 12; i++)
        {
            okImage.gameObject.transform.localPosition -= new Vector3(0, 2.4f, 0);
            okImage.gameObject.transform.localEulerAngles += new Vector3(0, 0, 1.5f);
            yield return new WaitForSeconds(0.01f);
        }*/

        okImage.gameObject.transform.DOPunchPosition(new Vector3(0, -1.5f, 0), 0.3f);
        yield return new WaitForSeconds(0.5f);

        yield break;
    }
}
