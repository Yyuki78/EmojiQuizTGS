using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShowIcon2 : MonoBehaviour
{
    [SerializeField] Image IconImage1 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage2 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage3 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage4 = null;//Emoji画像を表示させるImageオブジェクト
    public Image[] IconImage = new Image[4];

    private int i = 0;

    // Start is called before the first frame update
    void Awake()
    {
        IconImage[0] = IconImage1;
        IconImage[1] = IconImage2;
        IconImage[2] = IconImage3;
        IconImage[3] = IconImage4;

        IconImage1.gameObject.SetActive(false);
        IconImage2.gameObject.SetActive(false);
        IconImage3.gameObject.SetActive(false);
        IconImage4.gameObject.SetActive(false);
    }

    public IEnumerator showIcon()
    {
        var players = PhotonNetwork.PlayerList;
        //アイコンの生成
        i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.1f);
            if (player.IsMasterClient) continue;
            IconImage[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
            i++;
        }
        for (int j = 0; j < 4; j++)
        {
            IconImage[j].gameObject.SetActive(true);
        }

        yield break;
    }
}
