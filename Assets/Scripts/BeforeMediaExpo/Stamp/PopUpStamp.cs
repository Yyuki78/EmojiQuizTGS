using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpStamp : MonoBehaviour
{
    [SerializeField] Image EmojiImage = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    /*
    //stampImage1～10の浮き上がるオブジェクト
    [SerializeField] private Image PopupImage1;
    [SerializeField] private Image PopupImage2;
    [SerializeField] private Image PopupImage3;
    [SerializeField] private Image PopupImage4;
    [SerializeField] private Image PopupImage5;
    [SerializeField] private Image PopupImage6;
    [SerializeField] private Image PopupImage7;
    [SerializeField] private Image PopupImage8;
    [SerializeField] private Image PopupImage9;
    [SerializeField] private Image PopupImage10;*/

    //
    [SerializeField] GameObject canvas;
    public GameObject popupPrefab;
    private Image PrefabImage;
    private GameObject obj;

    //private StampManager stampManager;

    private void Awake()
    {
        obj = (GameObject)Instantiate(popupPrefab);
        PrefabImage = popupPrefab.GetComponent<Image>();

        //stampManager = GameObject.FindWithTag("StampManager").GetComponent<StampManager>();
    }

    //ボタンにこの関数を割り当てて使用
    public void PopUpStampButton()
    {
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;

        //ここでメッセージを送る？
        switch (button_ob.name)
        {
            case "stampImage1":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/1");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);
                //stampManager.Fire(button_ob.transform.position);

                break;
            case "stampImage2":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/2");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);
                //stampManager.Fire(button_ob.transform.position);

                break;
            case "stampImage3":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/3");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);
                //stampManager.Fire(button_ob.transform.position);

                break;
            case "stampImage4":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/4");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage5":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/5");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage6":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/6");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage7":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/7");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage8":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/8");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage9":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/9");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            case "stampImage10":
                PrefabImage.sprite = Resources.Load<Sprite>("StampImage/10");

                obj = Instantiate(popupPrefab);
                obj.transform.SetParent(button_ob.transform, false);

                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
