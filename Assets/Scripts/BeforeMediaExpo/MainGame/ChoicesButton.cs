using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class ChoicesButton : MonoBehaviour
{
    //選択肢のボタンを押すと、自分の解答が保存される

    //ボタンのモード(0は枠が切り替わる,1は押された画像になる)
    [SerializeField] int ButtonMode = 0;

    [SerializeField] GameObject MainGamePanel;
    ThemaGenerator _themaGenerator;

    // eventSystem型の変数を宣言　インスペクターにEventSystemをアタッチして取得しておく
    [SerializeField] private EventSystem eventSystem;

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    //選択したことが分かる枠画像
    [SerializeField] GameObject ChooseFlame;

    //選択した・されていないボタン画像
    [SerializeField] Sprite ButtonImage;
    [SerializeField] Sprite pushButtonImage;

    //ボタンの画像がある場所
    [SerializeField] GameObject[] Buttons = new GameObject[5];
    private Image[] buttonImages = new Image[5];
    private Transform[] choiceImages = new Transform[5];

    //選択された画像番号
    private int choiceNum;
    private int x;

    //音系
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Awake()
    {
        _themaGenerator = MainGamePanel.GetComponent<ThemaGenerator>();
        ChooseFlame.SetActive(false);
        _audio = AudioManager.GetComponent<AudioManager>();
        for (int i = 0; i < 5; i++)
        {
            buttonImages[i] = Buttons[i].GetComponent<Image>();
            var x = Buttons[i].GetComponentsInChildrenWithoutSelf<Transform>();
            choiceImages[i] = x[0];
        }
    }

    //ボタンにこの関数を割り当てて使用
    public void SendChoice()
    {
        _audio.SE1();

        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("選択されたのは" + button_ob.name);
        x = int.Parse(button_ob.name) - 1;
        choiceNum = _themaGenerator._choicesNum[x];
        Debug.Log("画像番号は" + choiceNum);
        PhotonNetwork.LocalPlayer.SetChoiceNum(choiceNum);

        switch (ButtonMode)
        {
            case 0:
                switch (int.Parse(button_ob.name))
                {
                    case 1:
                        ChooseFlame.transform.localPosition = new Vector3(-150, 125, 0);
                        break;
                    case 2:
                        ChooseFlame.transform.localPosition = new Vector3(150, 125, 0);
                        break;
                    case 3:
                        ChooseFlame.transform.localPosition = new Vector3(-300, -140, 0);
                        break;
                    case 4:
                        ChooseFlame.transform.localPosition = new Vector3(0, -140, 0);
                        break;
                    case 5:
                        ChooseFlame.transform.localPosition = new Vector3(300, -140, 0);
                        break;
                    default:
                        Debug.Log("押されたボタンが違います");
                        break;
                }
                ChooseFlame.SetActive(true);
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    buttonImages[i].sprite = ButtonImage;
                    choiceImages[i].localPosition = new Vector3(5.7f, 11f, 0);
                }

                buttonImages[int.Parse(button_ob.name) - 1].sprite = pushButtonImage;
                choiceImages[int.Parse(button_ob.name) - 1].localPosition = new Vector3(-5, 3, 0);

                break;
            default:
                Debug.Log("ButtonModeが違います");
                break;
        }
    }

    private void OnDisable()
    {
        switch (ButtonMode)
        {
            case 0:
                ChooseFlame.SetActive(false);
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    buttonImages[i].sprite = ButtonImage;
                    choiceImages[i].localPosition = new Vector3(5.7f, 11f, 0);
                }
                break;
            default:
                Debug.Log("ButtonModeが違います");
                break;
        }
    }
}
