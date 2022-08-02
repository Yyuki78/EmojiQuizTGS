using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieButton : MonoBehaviour
{
    [SerializeField] GameObject MoviePanel;

    VideoManager a;

    private void Awake()
    {
        a = MoviePanel.GetComponent<VideoManager>();
    }
    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        if(GameManager.Instance.GetCurrentState() == GameManager.GameMode.Start)
        {
            a.ReturnState = 0;
        }else if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.IconSelect)
        {
            a.ReturnState = 1;
        }else if(GameManager.Instance.GetCurrentState() == GameManager.GameMode.RoomSelect)
        {
            a.ReturnState = 2;
        }
        else
        {
            a.ReturnState = 3;
        }
        MoviePanel.SetActive(true);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Movie);
        Debug.Log("Movie��");  // ���O���o��
    }
}
