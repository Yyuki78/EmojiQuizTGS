using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShowIcon : MonoBehaviour
{
    [SerializeField] Image IconImage1 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage2 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage3 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage4 = null;//Emoji画像を表示させるImageオブジェクト
    [SerializeField] Image IconImage5 = null;//Emoji画像を表示させるImageオブジェクト
    public Image[] IconImages = new Image[5];

    private int i = 0;


    // Start is called before the first frame update
    void Awake()
    {
        IconImages[0] = IconImage1;
        IconImages[1] = IconImage2;
        IconImages[2] = IconImage3;
        IconImages[3] = IconImage4;
        IconImages[4] = IconImage5;
    }

    public IEnumerator showIcon()
    {
        var players = PhotonNetwork.PlayerList;
        //アイコンの生成
        i = 0;
        foreach (var player in players)
        {
            yield return new WaitForSeconds(0.1f);
            IconImages[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
            i++;
        }
        for (int j = 0; j < 5; j++)
        {
            IconImages[j].gameObject.SetActive(true);
        }

        yield break;
    }
}
