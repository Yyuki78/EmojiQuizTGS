using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ReadyButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspectorで設定する

    [SerializeField] GameObject alreadySelectPanel;
    [SerializeField] Image checkImage;
    [SerializeField] Image okImage;

    private bool isDirect = false;//クリック時に一度カーソルが重なる部分を経由したかどうか

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        alreadySelectPanel.SetActive(false);
        okImage.gameObject.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // ボタンが押される
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // ボタンが離される
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
        StartCoroutine(ClickEffect());
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //ボタンの範囲にマウスカーソルが入る
        transform.DOScale(1.1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1.05f, 0.24f).SetEase(Ease.OutCubic);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //ボタンの範囲からマウスカーソルが出る
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
    }

    private IEnumerator ClickEffect()
    {
        alreadySelectPanel.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            checkImage.fillAmount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            if (i >= 18)
            {
                okImage.gameObject.SetActive(true);
            }
        }
        checkImage.gameObject.SetActive(false);

        okImage.gameObject.transform.DOPunchPosition(new Vector3(0.01f, 0, 0), 0.12f);
        for (int i = 0; i < 12; i++)
        {
            okImage.gameObject.transform.localPosition += new Vector3(0, 2.4f, 0);
            okImage.gameObject.transform.localEulerAngles -= new Vector3(0, 0, 1.5f);
            yield return new WaitForSeconds(0.01f);
        }

        okImage.gameObject.transform.DOPunchPosition(new Vector3(0.01f, 0, 0), 0.12f);
        for (int i = 0; i < 12; i++)
        {
            okImage.gameObject.transform.localPosition -= new Vector3(0, 2.4f, 0);
            okImage.gameObject.transform.localEulerAngles += new Vector3(0, 0, 1.5f);
            yield return new WaitForSeconds(0.01f);
        }

        okImage.gameObject.transform.DOPunchPosition(new Vector3(0, -0.1f, 0), 0.3f);
        yield return new WaitForSeconds(0.5f);

        yield break;
    }
}
