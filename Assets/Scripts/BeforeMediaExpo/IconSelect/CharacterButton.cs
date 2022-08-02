using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] GameObject IconSelectPanel;
    [SerializeField] GameObject RoomSelectPanel1;
    [SerializeField] GameObject RoomSelectPanel2;
    [SerializeField] GameObject RoomSelectPanel3;
    [SerializeField] GameObject RoomSelectPanel4;
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        RoomSelectPanel1.SetActive(false);
        RoomSelectPanel2.SetActive(false);
        RoomSelectPanel3.SetActive(false);
        RoomSelectPanel4.SetActive(false);
        IconSelectPanel.SetActive(true);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
        Debug.Log("IconSelectへ");  // ログを出力
    }
}
