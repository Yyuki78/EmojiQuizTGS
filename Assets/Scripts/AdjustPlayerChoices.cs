using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AdjustPlayerChoices : MonoBehaviourPunCallbacks
{
    //èoëËé“âÊñ Ç≈âìöé“ÇÃâìöÇ™ï™Ç©ÇÈÇÊÇ§Ç…Ç∑ÇÈ

    [SerializeField] GameObject[] PlayerChoices = new GameObject[4];
    [SerializeField] Image[] ChoiceImages = new Image[4];

    private int num = 0;
    private bool once = false;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            ChoiceImages[i].sprite = Resources.Load<Sprite>("Image/129");
        }
    }

    private void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        if (MainGameController.mainmode != MainGameController.MainGameMode.QuestionTime) return;
        if (!once)
        {
            once = true;
            Setting();
            StartCoroutine(AdjustPlayerChice());
        }
    }

    private void Setting()
    {
        for (int i = 0; i < 4; i++)
        {
            ChoiceImages[i].sprite = Resources.Load<Sprite>("Image/129");
        }

        for (int i = 0; i < 4; i++)
        {
            PlayerChoices[i].gameObject.SetActive(false);
        }

        var players = PhotonNetwork.PlayerList;
        switch (players.Length)
        {
            case 2:
                PlayerChoices[0].transform.localPosition = new Vector3(-350, 0, 0);
                PlayerChoices[0].gameObject.SetActive(true);
                break;
            case 3:
                PlayerChoices[0].transform.localPosition = new Vector3(-350, 0, 0);
                PlayerChoices[1].transform.localPosition = new Vector3(350, 0, 0);
                PlayerChoices[0].gameObject.SetActive(true);
                PlayerChoices[1].gameObject.SetActive(true);
                break;
            case 4:
                PlayerChoices[0].transform.localPosition = new Vector3(-350, 125, 0);
                PlayerChoices[1].transform.localPosition = new Vector3(350, 125, 0);
                PlayerChoices[2].transform.localPosition = new Vector3(-350, -175, 0);
                PlayerChoices[0].gameObject.SetActive(true);
                PlayerChoices[1].gameObject.SetActive(true);
                PlayerChoices[2].gameObject.SetActive(true);
                break;
            case 5:
                PlayerChoices[0].transform.localPosition = new Vector3(-350, 125, 0);
                PlayerChoices[1].transform.localPosition = new Vector3(350, 125, 0);
                PlayerChoices[2].transform.localPosition = new Vector3(-350, -175, 0);
                PlayerChoices[3].transform.localPosition = new Vector3(350, -175, 0);
                PlayerChoices[0].gameObject.SetActive(true);
                PlayerChoices[1].gameObject.SetActive(true);
                PlayerChoices[2].gameObject.SetActive(true);
                PlayerChoices[3].gameObject.SetActive(true);
                break;
            default:
                Debug.Log("éQâ¡é“ÇÃêîÇ™àŸèÌÇ≈Ç∑");
                break;
        }
    }

    private IEnumerator AdjustPlayerChice()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            num = 0;
            var players = PhotonNetwork.PlayerList;
            foreach (var player in players)
            {
                yield return new WaitForSeconds(0.01f);
                if (player == PhotonNetwork.LocalPlayer) continue;
                ChoiceImages[num].sprite = Resources.Load<Sprite>("Image/" + player.GetChoiceNum());
                num++;
            }
        }
    }

    private void OnDisable()
    {
        once = false;
        StopAllCoroutines();
    }
}
