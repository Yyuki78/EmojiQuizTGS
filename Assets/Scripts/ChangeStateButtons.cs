using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateButtons : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickStartButton()
    {
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.Movie);
    }
}
