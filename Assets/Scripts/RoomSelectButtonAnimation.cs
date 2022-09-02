using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RoomSelectButtonAnimation : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int ButtonMode = 0; //inspectorで変更する

    private CanvasGroup _canvasGroup;

    public AudioManager _audio;//inspectorで設定する

    private bool isDirect = false;//クリック時に一度カーソルが重なる部分を経由したかどうか

    [SerializeField] GameObject[] DoorMassObj = new GameObject[2];

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
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
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
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
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
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, -30, 0);
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
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                DoorMassObj[ButtonMode].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                Debug.Log("ミス");
                break;
        }
    }
}
