using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
        Debug.Log("�����Skip!");  // ���O���o��
    }
}
