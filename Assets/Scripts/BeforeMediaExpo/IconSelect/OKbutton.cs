using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKbutton : MonoBehaviour
{
    [SerializeField] GameObject IconSelectPanel;
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        IconSelectPanel.SetActive(false);
        this.gameObject.SetActive(false);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.RoomSelect);
        Debug.Log("RoomSelect��");  // ���O���o��
    }
}
