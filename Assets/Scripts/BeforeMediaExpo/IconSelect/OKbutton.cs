using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKbutton : MonoBehaviour
{
    [SerializeField] GameObject IconSelectPanel;
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        IconSelectPanel.SetActive(false);
        this.gameObject.SetActive(false);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.RoomSelect);
        Debug.Log("RoomSelectへ");  // ログを出力
    }
}
