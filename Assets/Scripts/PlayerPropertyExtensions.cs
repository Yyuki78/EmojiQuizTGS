using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class PlayerPropertyExtensions
{
    private const string PLAYER_ASSIGN_NUMBER = "n";

    private static Hashtable _hashtable = new Hashtable();

    private const string ChoiceNumKey = "c";

    private static readonly Hashtable numToSet = new Hashtable();

    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
    //　プレイヤーの番号
    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

    //Hashtableにプレイヤーに割り振られた番号があれば取得する
    private static bool TryAndGetPlayerNum(this Hashtable hashtable, out int playerAssignNumber)
    {
        if (hashtable[PLAYER_ASSIGN_NUMBER] is int value)
        {
            playerAssignNumber = value;
            return true;
        }

        playerAssignNumber = 0;
        return false;
    }

    //プレイヤー番号を取得する
    public static int GetPlayerNum(this Player player)
    {
        player.CustomProperties.TryAndGetPlayerNum(out int playerNum);
        return playerNum;
    }

    //プレイヤーの割り当て番号のカスタムプロパティを更新する
    public static void UpdatePlayerNum(this Player player, int assignNum)
    {
        _hashtable[PLAYER_ASSIGN_NUMBER] = assignNum;
        player.SetCustomProperties(_hashtable);
        _hashtable.Clear();
    }

    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
    //　プレイヤーの選択肢
    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

    // プレイヤーの選択肢を取得する
    public static int GetChoiceNum(this Player player)
    {
        return (player.CustomProperties[ChoiceNumKey] is int choiceNum) ? choiceNum : 0;
    }

    // プレイヤーの選択肢を設定する
    public static void SetChoiceNum(this Player player, int choiceNum)
    {
        numToSet[ChoiceNumKey] = choiceNum;
        player.SetCustomProperties(numToSet);
        numToSet.Clear();
    }
}