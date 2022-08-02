using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    //[SerializeField] GameObject MoviePanel;
    //[SerializeField] GameObject ResultPanel;
    //[SerializeField] GameObject ChangeImage;
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
        Debug.Log("Resultへ遷移！");  // ログを出力
    }

    /*
    private IEnumerator Change()
    {
        ChangeImage.SetActive(true);
        // 指定秒間待つ
        yield return new WaitForSeconds(0.65f);

        GameManager.Instance.SetCurrentState(GameManager.GameMode.Result);
        MoviePanel.SetActive(false);
        ResultPanel.SetActive(true);
        Debug.Log("Resultへ遷移！");  // ログを出力
    }*/
}
