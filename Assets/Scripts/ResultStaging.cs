using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStaging : MonoBehaviour
{
    [SerializeField] GameObject[] BarImage = new GameObject[5];
    private Transform[] BarPos = new Transform[5];//�������̃o�[�̍��W
    private Image[] CorrectImage = new Image[5];//�������̃o�[�摜

    private int xPos = -250;//�L�΂��o�[��x���W

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            BarPos[i] = BarImage[i].GetComponent<Transform>();
            CorrectImage[i] = BarImage[i].GetComponent<Image>();
            CorrectImage[i].fillAmount = 0;
        }
    }

    public IEnumerator Staging(bool[] score, int num)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("�X�R�A�̕\�����n�߂܂�");

        for (int i = 0; i < 5; i++)
        {
            //������1������s������\������
            if (score[i] == true)
            {
                BarPos[i].localPosition = new Vector2(xPos, 200);
                //�����Ȃ��萔�ɍ������o�[��L�΂�
                for (int j = 0; j < 100; j++)
                {
                    CorrectImage[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.01f);
                }
                xPos += 150;
            }
            else
            {
                for (int j = 0; j < 100; j++)
                {
                    yield return new WaitForSeconds(0.01f);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }
}