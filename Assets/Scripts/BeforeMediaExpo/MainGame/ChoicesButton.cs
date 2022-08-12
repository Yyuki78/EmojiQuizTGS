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

    //選択したことが分かる枠画像
    [SerializeField] GameObject ChooseFlame;

    //選択された画像番号
    private int choiceNum;
    private int x;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
        ChooseFlame.SetActive(false);
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

        switch (int.Parse(button_ob.name))
        {
            case 1:
                ChooseFlame.transform.localPosition = new Vector3(-150, 125, 0);
                break;
            case 2:
                ChooseFlame.transform.localPosition = new Vector3(150, 125, 0);
                break;
            case 3:
                ChooseFlame.transform.localPosition = new Vector3(-300, -175, 0);
                break;
            case 4:
                ChooseFlame.transform.localPosition = new Vector3(0, -175, 0);
                break;
            case 5:
                ChooseFlame.transform.localPosition = new Vector3(300, -175, 0);
                break;
            default:
                Debug.Log("押されたボタンが違います");
                break;
        }
        ChooseFlame.SetActive(true);
    }

    private void OnDisable()
    {
        ChooseFlame.SetActive(false);
    }
}
