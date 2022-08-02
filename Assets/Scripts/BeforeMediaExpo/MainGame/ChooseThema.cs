using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChooseThema : MonoBehaviourPunCallbacks
{
    private ThemaGenerator _themaGenerator;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _themaGenerator = GetComponent<ThemaGenerator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.MainGame)
        {
            if (MainGameManager2.MainGameMode.PlayerSelect == MainGameManager2.mainmode)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    if (once == true)
                    {
                        Debug.Log("ñ‚ëËÇçÏê¨ÇµÇ‹Ç∑");
                        once = false;
                        _themaGenerator.ThemaGenerate();
                        _themaGenerator.ChoicesGenerate();
                        photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, _themaGenerator._themaNum, _themaGenerator._choicesNum);
                    }
                }
            }
        }
    }

    [PunRPC]
    private void RpcSendMessage(int thema, int[] choices)
    {
        StartCoroutine(Showchoice(thema, choices));
    }

    private IEnumerator Showchoice(int thema, int[] choices)
    {
        Debug.Log("Ç®ëËÇ∆ëIëéàÇéÛÇØéÊÇ¡ÇƒÇªÇÍÇºÇÍï\é¶ÅI");
        Debug.Log(thema + "," + choices[0] + "," + choices[1] + "," + choices[2] + "," + choices[3] + "," + choices[4]);
        once = false;
        yield return new WaitForSeconds(1.0f);
        once = false;
        _themaGenerator.Showchoices(thema, choices);
        yield break;
    }

    public void Init()
    {
        once = true;
    }
}
