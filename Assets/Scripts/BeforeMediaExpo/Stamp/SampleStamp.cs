using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;//Image型を扱うために導入

public class SampleStamp : MonoBehaviour
{
    [SerializeField] Image EmojiImage1 = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入
    [SerializeField] Image EmojiImage2 = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入
    [SerializeField] Image EmojiImage3 = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入
    [SerializeField] Image EmojiImage4 = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;

        Debug.Log(button_ob.name);
        switch (button_ob.name)
        {
            case "1":
                EmojiImage1.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1,11));
                break;
            case "2":
                EmojiImage2.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            case "3":
                EmojiImage3.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            case "4":
                EmojiImage4.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            default :
                Debug.Log("Default");
                break;
        }
        
    }
}
