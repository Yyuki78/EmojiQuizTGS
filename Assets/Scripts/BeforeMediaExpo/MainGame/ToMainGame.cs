using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMainGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToGame()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.MainGame);
    }
}
