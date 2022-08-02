using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class ChoicesButton : MonoBehaviour
{
    //選択肢のボタンを押すと、自分の解答が保存される

    [SerializeField] GameObject MainGamePanel;
    ThemaGenerator _themaGenerator;

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    //選択された画像番号
    private int choiceNum;
    private int x;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
    }

    //ボタンにこの関数を割り当てて使用
    public void SendChoice()
    {
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("選択されたのは" + button_ob.name);
        x = int.Parse(button_ob.name) - 1;
        choiceNum = _themaGenerator._choicesNum[x];
        Debug.Log("画像番号は" + choiceNum);
        PhotonNetwork.LocalPlayer.SetChoiceNum(choiceNum);
    }
}
