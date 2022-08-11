using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemaGenerator : MonoBehaviour
{
    //����̕\���p
    [SerializeField] Image EmojiImage = null;

    //�I�����̕\���p
    [SerializeField] Image EmojiImage1 = null;
    [SerializeField] Image EmojiImage2 = null;
    [SerializeField] Image EmojiImage3 = null;
    [SerializeField] Image EmojiImage4 = null;
    [SerializeField] Image EmojiImage5 = null;

    private EmojiInformation emojiInfo;//CSV��ǂݍ���EmojiInformation�N���X���������߂ɐ錾

    public int _themaNum;//����̔ԍ�
    public byte[] _themaBytes;//����̔ԍ���byte�^

    private int _ranChoice;
    public int[] _choicesNum;//�I�����̔ԍ�
    public byte[] _choicesBytes1;//�I����1��byte�^
    public byte[] _choicesBytes2;//�I����2��byte�^
    public byte[] _choicesBytes3;//�I����3��byte�^
    public byte[] _choicesBytes4;//�I����4��byte�^
    public byte[] _choicesBytes5;//�I����5��byte�^

    //�����̑I�����ԍ�
    public int CorrectPos;

    // Start is called before the first frame update
    private void Start()
    {
        emojiInfo = new EmojiInformation();//EmojiInformation�N���X�̎��̂Ƃ���emojiInfo����
        emojiInfo.Init();//CSV�f�[�^�̓ǂݍ��݂ƕϐ��ւ̊i�[����
    }

    //�����ł���̐����𐶐�����
    public void ThemaGenerate()
    {
        _themaNum = UnityEngine.Random.Range(1, 128);
        Debug.Log("�����" + (_themaNum + 1));
        Debug.Log("����̏���" + emojiInfo.emojiAttribute1[_themaNum] + "," + emojiInfo.emojiAttribute2[_themaNum] + "," + emojiInfo.imageAddress[_themaNum]);
        _themaBytes = BitConverter.GetBytes(_themaNum);
        foreach (byte b in _themaBytes)
        {
            Debug.Log(string.Format("{0,3:X2}", b));
        }
        //EmojiImage.sprite = Resources.Load<Sprite>(emojiInfo.imageAddress[_themaNum]);
    }

    //�����őI�����̐����𐶐�����
    public void ChoicesGenerate()
    {
        _choicesNum = new int[5];
        _choicesNum[0] = _themaNum;

        //null���p�̃v���O����
        for(int i = 1; i < 5; i++)
        {
            _choicesNum[i] = _themaNum;
        }
        /*
        //��荇�����̑I�������o�p�v���O����
        //���������Ȃ��悤�ɂ��Ă��邾���ł���Ƃ̑��ւ͂Ȃ�
        for(int i = 1; i < 5; i++)
        {
            do
            {
                _ranChoice = UnityEngine.Random.Range(1, 129);
            } while (_ranChoice == _themaNum||_ranChoice==_choicesNum[1] || _ranChoice == _choicesNum[2] || _ranChoice == _choicesNum[3] || _ranChoice == _choicesNum[4]);
            _choicesNum[i] = _ranChoice;
        }
        //�z��̕��т��V���b�t������
        for (int i = 0; i < 5; i++)
        {
            int temp = _choicesNum[i];
            int randomIndex = UnityEngine.Random.Range(0, 5);
            _choicesNum[i] = _choicesNum[randomIndex];
            _choicesNum[randomIndex] = temp;
        }
        //�I������Byte�^�ɕϊ�
        _choicesBytes1 = BitConverter.GetBytes(_choicesNum[0]);
        _choicesBytes2 = BitConverter.GetBytes(_choicesNum[1]);
        _choicesBytes3 = BitConverter.GetBytes(_choicesNum[2]);
        _choicesBytes4 = BitConverter.GetBytes(_choicesNum[3]);
        _choicesBytes5 = BitConverter.GetBytes(_choicesNum[4]);
        //�m�F�p
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i + "�Ԗڂ�" + _choicesNum[i]);
        }*/

        //����Ɏ��Ă���I�����̑I�o
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

        //����Ɗ�����Ă���I�����̑I�o1
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

        //����Ɗ�����Ă���I�����̑I�o2
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

        //���S�Ƀ����_���ȑI�����̑I�o
        do
        {
            _choicesNum[4] = UnityEngine.Random.Range(1, 129);
        } while (_choicesNum[0] == _choicesNum[4] || _choicesNum[1] == _choicesNum[4] || _choicesNum[2] == _choicesNum[4] || _choicesNum[3] == _choicesNum[4]);
        
        //�z��̕��т��V���b�t������
        for (int i = 0; i < 5; i++)
        {
            int temp = _choicesNum[i];
            int randomIndex = UnityEngine.Random.Range(0, 5);
            _choicesNum[i] = _choicesNum[randomIndex];
            _choicesNum[randomIndex] = temp;
        }

        /*
        //�I������Byte�^�ɕϊ�
        _choicesBytes1 = BitConverter.GetBytes(_choicesNum[0]);
        _choicesBytes2 = BitConverter.GetBytes(_choicesNum[1]);
        _choicesBytes3 = BitConverter.GetBytes(_choicesNum[2]);
        _choicesBytes4 = BitConverter.GetBytes(_choicesNum[3]);
        _choicesBytes5 = BitConverter.GetBytes(_choicesNum[4]);
        */

        //�m�F�p
        for (int i = 0; i < 5; i++)
        {
            Debug.Log((i + 1) + "�Ԗڂ�" + _choicesNum[i]);
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

    //�������󂯎���ĕ\��
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
