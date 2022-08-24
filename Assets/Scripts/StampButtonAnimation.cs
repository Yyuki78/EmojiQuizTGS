using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class StampButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int ButtonMode = 0; //inspectorで変更する

    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspectorで設定する

    private bool isDirect = false;//クリック時に一度カーソルが重なる部分を経由したかどうか

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // ボタンが押される
        switch (ButtonMode)
        {
            case 0:
                transform.DOScale(0.95f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(0.8f, 0.24f).SetEase(Ease.OutCubic);

                _audio.SE1();
                break;
            case 1:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                if (isDirect)
                {
                    transform.DORotate(new Vector3(0, 0, -120), 0.24f);
                    transform.DORotate(new Vector3(0, 0, -240), 0.24f);
                }
                else
                {
                    transform.DORotate(new Vector3(0, 0, -240), 0.24f);
                }

                _audio.SE1();
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // ボタンが離される
        switch (ButtonMode)
        {
            case 0:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                transform.DORotate(new Vector3(0, 0, 360), 0.24f);
                isDirect = true;
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
        switch (ButtonMode)
        {
            case 0:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                transform.DORotate(new Vector3(0, 0, 360), 0.24f);
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //ボタンの範囲にマウスカーソルが入る
        switch (ButtonMode)
        {
            case 0:
                transform.DOScale(1.1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1.05f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
                transform.DOScale(1.05f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1.025f, 0.24f).SetEase(Ease.OutCubic);
                transform.DORotate(new Vector3(0, 0, -120), 0.24f);
                isDirect = false;
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //ボタンの範囲からマウスカーソルが出る
        switch (ButtonMode)
        {
            case 0:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            case 1:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
                transform.DORotate(new Vector3(0, 0, 0), 0.24f);
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }
}
