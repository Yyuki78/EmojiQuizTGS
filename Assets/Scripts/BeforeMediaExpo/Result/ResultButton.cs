using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    //[SerializeField] GameObject MoviePanel;
    //[SerializeField] GameObject ResultPanel;
    //[SerializeField] GameObject ChangeImage;
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
        Debug.Log("Result�֑J�ځI");  // ���O���o��
    }

    /*
    private IEnumerator Change()
    {
        ChangeImage.SetActive(true);
        // �w��b�ԑ҂�
        yield return new WaitForSeconds(0.65f);

        GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
        MoviePanel.SetActive(false);
        ResultPanel.SetActive(true);
        Debug.Log("Result�֑J�ځI");  // ���O���o��
    }*/
}
