using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;//Image型を扱うために導入
public class IconSelect : MonoBehaviour
{
    [SerializeField] Image EmojiImage = null;//Emoji画像を表示させるImageオブジェクトとの連携のために導入

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    //GameObject型の変数を宣言
    [SerializeField] GameObject OKbutton;

    //ボタンにこの関数を割り当てて使用
    public void ChangeButtonTextName()
    {
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;

        Debug.Log(button_ob.name);
        EmojiImage.sprite = Resources.Load<Sprite>("Image/"+ button_ob.name);
        OKbutton.SetActive(true);
        //Playerの名前とスコアを絵文字番号にする
        PhotonNetwork.NickName = button_ob.name;
        PhotonNetwork.LocalPlayer.SetScore(int.Parse(button_ob.name));
    }
    /*
    //「1」ボタンが押された時の動作。Imageオブジェクトに1の画像を表示させる
    public void ClickButton1()
    {
        Debug.Log(this.gameObject.name);
        //Assets直下のResourcesフォルダ直下にあるImage/1と言うパスで表されるファイルを、変数EmojiImageと結び付けられたオブジェクトに表示させる処理。.pngなどの拡張子は不要
        EmojiImage.sprite = Resources.Load<Sprite>("Image/1");
    }
    //「2」ボタンが押された時の動作。Imageオブジェクトに2の画像を表示させる
    public void ClickButton2()
    {
        //Assets直下のResourcesフォルダ直下にあるImage/pipo-enemy011と言うパスで表されるファイルを、変数EnemyImageと結び付けられたオブジェクトに表示させる処理。.pngなどの拡張子は不要
        EmojiImage.sprite = Resources.Load<Sprite>("Image/2");
    }
    //「3」ボタンが押された時の動作。Imageオブジェクトにドラゴンの画像を表示させる
    public void ClickButton3()
    {
        //Assets直下のResourcesフォルダ直下にあるImage/pipo-enemy021と言うパスで表されるファイルを、変数EnemyImageと結び付けられたオブジェクトに表示させる処理。.pngなどの拡張子は不要
        EmojiImage.sprite = Resources.Load<Sprite>("Image/3");
    }
    //「4」ボタンが押された時の動作。Imageオブジェクトにドラゴンの画像を表示させる
    public void ClickButton4()
    {
        //Assets直下のResourcesフォルダ直下にあるImage/pipo-enemy021と言うパスで表されるファイルを、変数EnemyImageと結び付けられたオブジェクトに表示させる処理。.pngなどの拡張子は不要
        EmojiImage.sprite = Resources.Load<Sprite>("Image/4");
    }
    */
}