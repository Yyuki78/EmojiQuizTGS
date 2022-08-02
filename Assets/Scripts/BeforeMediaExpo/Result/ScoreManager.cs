using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //�z����󂯎���ăX�R�A��\������X�N���v�g

    [SerializeField] Sprite correctImage;//�����摜
    [SerializeField] Sprite incorrectImage;//�s�����摜

    //scoreImage1�`10�̃I�u�W�F�N�g
    [SerializeField] private Image ScoreImage1;
    [SerializeField] private Image ScoreImage2;
    [SerializeField] private Image ScoreImage3;
    [SerializeField] private Image ScoreImage4;
    [SerializeField] private Image ScoreImage5;
    [SerializeField] private Image ScoreImage6;
    [SerializeField] private Image ScoreImage7;
    [SerializeField] private Image ScoreImage8;
    [SerializeField] private Image ScoreImage9;
    [SerializeField] private Image ScoreImage10;
    private Image[] scoreImage;

    // Start is called before the first frame update
    void Awake()
    {
        scoreImage = new Image[]
        {
            ScoreImage1,ScoreImage2,ScoreImage3,ScoreImage4,ScoreImage5,ScoreImage6,ScoreImage7,ScoreImage8,ScoreImage9,ScoreImage10
        };
    }

    public void show(bool[] score)
    {
        StartCoroutine(Show(score));
    }

    //����X�R�A��\�����Ă����R���[�`��
    private IEnumerator Show(bool[] score)//�����Ŏ󂯎��̂�10��bool
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("�X�R�A�̕\�����n�߂܂�");

        for(int i = 0; i < 10; i++)
        {
            //������1������s������\������
            if (score[i] == true)
            {
                //�����摜��i�Ԗڂ̉摜�ɓ����
                scoreImage[i].sprite = correctImage;
            }
            else
            {
                //�s�����摜��i�Ԗڂ̉摜�ɓ����
                scoreImage[i].sprite = incorrectImage;
            }
            yield return new WaitForSeconds(0.75f);
        }
        yield break;
    }
}
