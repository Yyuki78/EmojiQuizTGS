using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieButton2 : MonoBehaviour
{
    //���[�����ɂ��郀�[�r�[�{�^��
    [SerializeField] GameObject MoviePanel;
    [SerializeField] GameObject Room1Panel;

    VideoManager a;

    private void Awake()
    {
        a = MoviePanel.GetComponent<VideoManager>();
    }
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        a.ReturnState = 3;
        Room1Panel.SetActive(false);
        MoviePanel.SetActive(true);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Movie);
        Debug.Log("Movie��");  // ���O���o��
    }
}
