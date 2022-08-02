using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReadyButton : MonoBehaviourPunCallbacks
{
    public void Onclick()
    {
        photonView.RPC(nameof(GoGame), RpcTarget.All);
    }

    [PunRPC]
    private void GoGame()
    {
        GameManager.Instance.SetCurrentState(GameManager.GameMode.MainGame);
    }
}
