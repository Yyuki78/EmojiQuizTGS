using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInRoomButton : MonoBehaviour
{
    public void toinroom()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.InRoom);
    }
}
