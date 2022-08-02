using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label = default;

    [SerializeField] Image EmojiImage1 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g
    [SerializeField] Image EmojiImage2 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g
    [SerializeField] Image EmojiImage3 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g
    [SerializeField] Image EmojiImage4 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g
    [SerializeField] Image EmojiImage5 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g
    public Image[] IconImage = new Image[5];
    private int i = 0;

    private StringBuilder builder;
    private float elapsedTime;

    private void Start()
    {
        builder = new StringBuilder();
        elapsedTime = 0f;
        IconImage[0] = EmojiImage1;
        IconImage[1] = EmojiImage2;
        IconImage[2] = EmojiImage3;
        IconImage[3] = EmojiImage4;
        IconImage[4] = EmojiImage5;
    }

    /*
    private void Update()
    {
        // �܂����[���ɎQ�����Ă��Ȃ��ꍇ�͍X�V���Ȃ�
        if (!PhotonNetwork.InRoom) { return; }

        // 1.0�b���Ƀe�L�X�g���X�V����
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1.0f)
        {
            elapsedTime = 0f;
            UpdateLabel();
        }
    }*/

    public void UpdateLabel()
    {
        var players = PhotonNetwork.PlayerList;
        /*
        Array.Sort(
            players,
            (p1, p2) =>
            {
                // �X�R�A���������Ƀ\�[�g����
                int diff = p2.GetScore() - p1.GetScore();
                if (diff != 0)
                {
                    return diff;
                }
                // �X�R�A�������������ꍇ�́AID�����������Ƀ\�[�g����
                return p1.ActorNumber - p2.ActorNumber;
            }
        );*/

        builder.Clear();
        i = 0;
        foreach (var player in players)
        {
            builder.AppendLine($"{player.NickName}({player.ActorNumber}) - {player.GetScore()}");
            IconImage[i].sprite = Resources.Load<Sprite>("Image/" + player.GetScore());
            i++;
        }
        label.text = builder.ToString();
    }
}
