using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Movie);
        Debug.Log("����̍Đ�!");  // ���O���o��
    }
}
