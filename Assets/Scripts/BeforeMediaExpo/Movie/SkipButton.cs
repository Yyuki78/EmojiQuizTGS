using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.IconSelect);
        Debug.Log("動画はSkip!");  // ログを出力
    }
}
