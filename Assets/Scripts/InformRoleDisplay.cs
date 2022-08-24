using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InformRoleDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField] Image QuestionerFlameImage;
    [SerializeField] Image QuestionerIconImage;
    [SerializeField] Image[] IconImages = new Image[4];
    [SerializeField] Image[] IconFlameImages = new Image[4];

    [SerializeField] Sprite myIconFlame;
    [SerializeField] Sprite otherIconFlame;

    private bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }

    private void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() != DebugGameManager.GameMode.MainGame) return;
        if (MainGameController.mainmode == MainGameController.MainGameMode.ReportAnswer)
        {
            if (!once)
            {
                once = true;
                Setting();
            }
        }
    }

    private void Setting()
    {
        QuestionerFlameImage.sprite = otherIconFlame;
        QuestionerIconImage.sprite = Resources.Load<Sprite>("Image/129");
        for (int i = 0; i < 4; i++)
        {
            IconFlameImages[i].sprite = otherIconFlame;
            IconImages[i].sprite = Resources.Load<Sprite>("Image/129");
            IconFlameImages[i].gameObject.SetActive(false);
        }

        var players = PhotonNetwork.PlayerList;
        switch (players.Length)
        {
            case 2:
                IconFlameImages[0].gameObject.transform.localPosition = new Vector3(0, -50, 0);
                IconFlameImages[0].gameObject.SetActive(true);
                break;
            case 3:
                IconFlameImages[0].gameObject.transform.localPosition = new Vector3(-200, -50, 0);
                IconFlameImages[1].gameObject.transform.localPosition = new Vector3(200, -50, 0);
                IconFlameImages[0].gameObject.SetActive(true);
                IconFlameImages[1].gameObject.SetActive(true);
                break;
            case 4:
                IconFlameImages[0].gameObject.transform.localPosition = new Vector3(-250, -50, 0);
                IconFlameImages[1].gameObject.transform.localPosition = new Vector3(0, -50, 0);
                IconFlameImages[2].gameObject.transform.localPosition = new Vector3(250, -50, 0);
                IconFlameImages[0].gameObject.SetActive(true);
                IconFlameImages[1].gameObject.SetActive(true);
                IconFlameImages[2].gameObject.SetActive(true);
                break;
            case 5:
                IconFlameImages[0].gameObject.transform.localPosition = new Vector3(-300, -50, 0);
                IconFlameImages[1].gameObject.transform.localPosition = new Vector3(-100, -50, 0);
                IconFlameImages[2].gameObject.transform.localPosition = new Vector3(100, -50, 0);
                IconFlameImages[3].gameObject.transform.localPosition = new Vector3(300, -50, 0);
                IconFlameImages[0].gameObject.SetActive(true);
                IconFlameImages[1].gameObject.SetActive(true);
                IconFlameImages[2].gameObject.SetActive(true);
                IconFlameImages[3].gameObject.SetActive(true);
                break;
            default:
                Debug.Log("éQâ¡é“ÇÃêîÇ™àŸèÌÇ≈Ç∑");
                break;
        }
    }

    public IEnumerator showIcon(Player Qplayer)
    {
        QuestionerIconImage.sprite = Resources.Load<Sprite>("IconImage/" + Qplayer.GetScore());
        if (PhotonNetwork.LocalPlayer == Qplayer)
        {
            QuestionerFlameImage.sprite = myIconFlame;
        }

        int num = 0;
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.01f);
            if (player == Qplayer) continue;
            IconImages[num].sprite = Resources.Load<Sprite>("IconImage/" + player.GetScore());
            if (PhotonNetwork.LocalPlayer == player)
            {
                IconFlameImages[num].sprite = myIconFlame;
            }
            num++;
        }

        yield return new WaitForSeconds(5f);
        once = false;

        yield break;
    }
}
