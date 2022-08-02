using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieButton2 : MonoBehaviour
{
    //ルーム内にあるムービーボタン
    [SerializeField] GameObject MoviePanel;
    [SerializeField] GameObject Room1Panel;

    VideoManager a;

    private void Awake()
    {
        a = MoviePanel.GetComponent<VideoManager>();
    }
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        a.ReturnState = 3;
        Room1Panel.SetActive(false);
        MoviePanel.SetActive(true);
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Movie);
        Debug.Log("Movieへ");  // ログを出力
    }
}
