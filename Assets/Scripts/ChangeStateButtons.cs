using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStateButtons : MonoBehaviour
{
    [SerializeField] GameObject SelectPanel;
    public void OnClickStartButton()
    {
        SelectPanel.SetActive(true);
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.7f);
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.Movie);
        yield break;
    }
}
