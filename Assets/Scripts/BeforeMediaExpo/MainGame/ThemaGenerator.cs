using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemaGenerator : MonoBehaviour
{
    //お題の表示用
    [SerializeField] Image EmojiImage = null;

    //選択肢の表示用
    [SerializeField] Image EmojiImage1 = null;
    [SerializeField] Image EmojiImage2 = null;
    [SerializeField] Image EmojiImage3 = null;
    [SerializeField] Image EmojiImage4 = null;
    [SerializeField] Image EmojiImage5 = null;

    private EmojiInformation emojiInfo;//CSVを読み込むEmojiInformationクラスを扱うために宣言

    public int _themaNum;//お題の番号
    public byte[] _themaBytes;//お題の番号のbyte型

    private int _ranChoice;
    public int[] _choicesNum;//選択肢の番号
    public byte[] _choicesBytes1;//選択肢1のbyte型
    public byte[] _choicesBytes2;//選択肢2のbyte型
    public byte[] _choicesBytes3;//選択肢3のbyte型
    public byte[] _choicesBytes4;//選択肢4のbyte型
    public byte[] _choicesBytes5;//選択肢5のbyte型

    //答えの選択肢番号
    public int CorrectPos;

    // Start is called before the first frame update
    private void Start()
    {
        emojiInfo = new EmojiInformation();//EmojiInformationクラスの実体としてemojiInfo生成
        emojiInfo.Init();//CSVデータの読み込みと変数への格納処理
    }

    //ここでお題の数字を生成する
    public void ThemaGenerate()
    {
        _themaNum = UnityEngine.Random.Range(1, 128);
        Debug.Log("お題は" + (_themaNum + 1));
        Debug.Log("お題の情報は" + emojiInfo.emojiAttribute1[_themaNum] + "," + emojiInfo.emojiAttribute2[_themaNum] + "," + emojiInfo.imageAddress[_themaNum]);
        _themaBytes = BitConverter.GetBytes(_themaNum);
        foreach (byte b in _themaBytes)
        {
            Debug.Log(string.Format("{0,3:X2}", b));
        }
        //EmojiImage.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_themaNum]);
    }

    //ここで選択肢の数字を生成する
    public void ChoicesGenerate()
    {
        _choicesNum = new int[5];
        _choicesNum[0] = _themaNum;

        //null回避用のプログラム
        for(int i = 1; i < 5; i++)
        {
            _choicesNum[i] = _themaNum;
        }
        /*
        //取り合えずの選択肢抽出用プログラム
        //数字が被らないようにしているだけでお題との相関はない
        for(int i = 1; i < 5; i++)
        {
            do
            {
                _ranChoice = UnityEngine.Random.Range(1, 129);
            } while (_ranChoice == _themaNum||_ranChoice==_choicesNum[1] || _ranChoice == _choicesNum[2] || _ranChoice == _choicesNum[3] || _ranChoice == _choicesNum[4]);
            _choicesNum[i] = _ranChoice;
        }
        //配列の並びをシャッフルする
        for (int i = 0; i < 5; i++)
        {
            int temp = _choicesNum[i];
            int randomIndex = UnityEngine.Random.Range(0, 5);
            _choicesNum[i] = _choicesNum[randomIndex];
            _choicesNum[randomIndex] = temp;
        }
        //選択肢をByte型に変換
        _choicesBytes1 = BitConverter.GetBytes(_choicesNum[0]);
        _choicesBytes2 = BitConverter.GetBytes(_choicesNum[1]);
        _choicesBytes3 = BitConverter.GetBytes(_choicesNum[2]);
        _choicesBytes4 = BitConverter.GetBytes(_choicesNum[3]);
        _choicesBytes5 = BitConverter.GetBytes(_choicesNum[4]);
        //確認用
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i + "番目は" + _choicesNum[i]);
        }*/

        //お題に似ている選択肢の選出
        do
        {
            int _attribute2 = emojiInfo.emojiAttribute2[_themaNum];
            int[] match = new int[6];
            int n = 0;

            for (int i = 0; i < emojiInfo.emojiID.Length; i++)
            {
                if (_attribute2 == emojiInfo.emojiAttribute2[i])
                {
                    match[n] = i;
                    n++;
                }
            }
            int rnd = UnityEngine.Random.Range(0, n);
            _choicesNum[1] = emojiInfo.emojiID[match[rnd]];
        } while (_choicesNum[0] == _choicesNum[1]);

        //お題と感情が似ている選択肢の選出1
        do
        {
            int _attribute1 = emojiInfo.emojiAttribute1[_themaNum];
            int[] match = new int[100];
            int n = 0;

            for (int i = 0; i < emojiInfo.emojiID.Length; i++)
            {
                if (_attribute1 == emojiInfo.emojiAttribute1[i])
                {
                    match[n] = i;
                    n++;
                }
            }
            int rnd = UnityEngine.Random.Range(0, n);
            _choicesNum[2] = emojiInfo.emojiID[match[rnd]];
        } while (_choicesNum[0] == _choicesNum[2] || _choicesNum[1] == _choicesNum[2]);

        //お題と感情が似ている選択肢の選出2
        do
        {
            int _attribute1 = emojiInfo.emojiAttribute1[_themaNum];
            int[] match = new int[100];
            int n = 0;

            for (int i = 0; i < emojiInfo.emojiID.Length; i++)
            {
                if (_attribute1 == emojiInfo.emojiAttribute1[i])
                {
                    match[n] = i;
                    n++;
                }
            }
            int rnd = UnityEngine.Random.Range(0, n);
            _choicesNum[3] = emojiInfo.emojiID[match[rnd]];
        } while (_choicesNum[0] == _choicesNum[3] || _choicesNum[1] == _choicesNum[3] || _choicesNum[2] == _choicesNum[3]);

        //完全にランダムな選択肢の選出
        do
        {
            _choicesNum[4] = UnityEngine.Random.Range(1, 129);
        } while (_choicesNum[0] == _choicesNum[4] || _choicesNum[1] == _choicesNum[4] || _choicesNum[2] == _choicesNum[4] || _choicesNum[3] == _choicesNum[4]);
        
        //配列の並びをシャッフルする
        for (int i = 0; i < 5; i++)
        {
            int temp = _choicesNum[i];
            int randomIndex = UnityEngine.Random.Range(0, 5);
            _choicesNum[i] = _choicesNum[randomIndex];
            _choicesNum[randomIndex] = temp;
        }

        /*
        //選択肢をByte型に変換
        _choicesBytes1 = BitConverter.GetBytes(_choicesNum[0]);
        _choicesBytes2 = BitConverter.GetBytes(_choicesNum[1]);
        _choicesBytes3 = BitConverter.GetBytes(_choicesNum[2]);
        _choicesBytes4 = BitConverter.GetBytes(_choicesNum[3]);
        _choicesBytes5 = BitConverter.GetBytes(_choicesNum[4]);
        */

        //確認用
        for (int i = 0; i < 5; i++)
        {
            Debug.Log((i + 1) + "番目は" + _choicesNum[i]);
            if (_choicesNum[i] == _themaNum)
            {
                CorrectPos = i + 1;
            }
        }
        /*
        EmojiImage1.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_choicesNum[0]]);
        EmojiImage2.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_choicesNum[1]]);
        EmojiImage3.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_choicesNum[2]]);
        EmojiImage4.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_choicesNum[3]]);
        EmojiImage5.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_choicesNum[4]]);
        */
    }

    //数字を受け取って表示
    public void Showchoices(int thema, int[] choices)
    {
        _themaNum = thema;
        _choicesNum = choices;
        EmojiImage.sprite = Resources.Load<Sprite>("Image/" + thema);
        EmojiImage1.sprite = Resources.Load<Sprite>("Image/" + choices[0]);
        EmojiImage2.sprite = Resources.Load<Sprite>("Image/" + choices[1]);
        EmojiImage3.sprite = Resources.Load<Sprite>("Image/" + choices[2]);
        EmojiImage4.sprite = Resources.Load<Sprite>("Image/" + choices[3]);
        EmojiImage5.sprite = Resources.Load<Sprite>("Image/" + choices[4]);
    }
}
