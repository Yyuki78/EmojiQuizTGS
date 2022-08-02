using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Movie);
        Debug.Log("動画の再生!");  // ログを出力
    }
}
