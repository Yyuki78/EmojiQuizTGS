using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//����ǂݍ���StringReader���g�p���邽�߂ɓ���
//�G�����̊�b����CSV����ǂݍ���ŁA�ϐ��Ɋi�[����
public class newEmojiInformation//MonoBehaviour�͌p�����Ȃ�
{
    static TextAsset csvFile;//CSV�t�@�C����ϐ��Ƃ��Ĉ������߂ɐ錾
    static List<string[]> emojiData = new List<string[]>();//CSV�t�@�C���̒��g������z����`�B�S�Ẵf�[�^��������`���Ŋi�[�����
    //�ϐ���[i]���G����ID��i�̏������ꂼ�ꎦ��
    public int[] emojiID = new int[101];//�G������ID
    public int[] emojiAttribute1 = new int[101];//�G�����̑���1�B����(��{���y�{���{���̑�(�����A���ꓙ)��1�`6�ŕ\��)
    public int[] emojiAttribute2 = new int[101];//�G�����̑���2�B�\��(20�ŕ������̕��ނ�4�ȏ�)
    public int[] emojiAttribute3 = new int[101];//�G�����̑���3�B�������Ă������(��̑�����2�`��)
    public string[] imageAddress = new string[101];//�G�����̉摜�C���[�W�̃A�h���X
    //�w�肵���A�h���X�ɕۊǂ���Ă���CSV�t�@�C���������ǂݎ��AemojiData�ɏ��𕶎���Ƃ��Ċi�[���郁�\�b�h�B
    //emojiData[i][j]��CSV�t�@�C����i�s�Aj��ڂ̃f�[�^��\���B�A���擪�s�i�^�C�g�������j��0�s�ڂƍl������̂Ƃ���B
    static void CsvReader()
    {
        csvFile = Resources.Load("CSV/newEmoji") as TextAsset;//�w�肵���t�@�C����TextAsset�Ƃ��ēǂݍ���(�t�@�C������.csv�͕s�v�Ȃ��Ƃɒ���)�@�ŏ��̍s�i�^�C�g�������j���ǂݍ��܂��̂ł����͎g�p���Ȃ�
        StringReader reader = new StringReader(csvFile.text);//
        while (reader.Peek() != -1)//�Ō�܂œǂݍ��ނ�-1�ɂȂ�֐�
        {
            string line = reader.ReadLine();//��s���ǂݍ���
            emojiData.Add(line.Split(','));//,��؂�Ń��X�g�ɒǉ����Ă���
        }
    }

    //emojiData�Ɉ�xCSV�t�@�C���̃f�[�^��ǂݍ��񂾂瑼�̃v���O�������爵���₷���悤��`����emojiID���̕ϐ��Ƀf�[�^���i�[����
    public void Init()
    {
        //�z���������
        emojiID = new int[101];
        emojiAttribute1 = new int[101];
        emojiAttribute2 = new int[101];
        emojiAttribute3 = new int[101];
        imageAddress = new string[101];

        CsvReader();//emojiData�֏����ꎞ�i�[

        int maxNum = emojiData.Count;
        if (emojiData.Count > 101)
        {
            maxNum = 101;
        }

        //�e�ϐ��փf�[�^���i�[
        for (int i = 1; i < maxNum; i++)//�G����ID���L�q���ꂽ�Ō�܂œǂݍ��݁B��s�ڂ̓^�C�g���Ȃ̂�i=0�̓f�[�^�Ƃ��Ĉ���Ȃ�
        {
            emojiID[i] = int.Parse(emojiData[i][0]);//string�^����int�^�֕ϊ�
            emojiAttribute1[i] = int.Parse(emojiData[i][1]);
            emojiAttribute2[i] = int.Parse(emojiData[i][2]);
            emojiAttribute3[i] = int.Parse(emojiData[i][3]);
            imageAddress[i] = emojiData[i][4];
        }
    }
}
