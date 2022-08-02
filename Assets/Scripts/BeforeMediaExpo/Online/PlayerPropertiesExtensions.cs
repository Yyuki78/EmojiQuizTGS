using ExitGames.Client.Photon;
using Photon.Realtime;

public static class PlayerPropertiesExtensions
{
    private const string ScoreKey = "Score";

    private static readonly Hashtable propsToSet = new Hashtable();

    /*
    private const string ChoiceNumKey = "ChoiceNum";

    private static readonly Hashtable numToSet = new Hashtable();
    */

    // �v���C���[�̃X�R�A���擾����
    public static int GetScore(this Player player)
    {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    // �v���C���[�̃X�R�A��ݒ肷��
    public static void SetScore(this Player player, int score)
    {
        propsToSet[ScoreKey] = score;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    /*
    // �v���C���[�̑I�������擾����
    public static int GetChoiceNum(this Player player)
    {
        return (player.CustomProperties[ChoiceNumKey] is int choiceNum) ? choiceNum : 0;
    }

    // �v���C���[�̑I������ݒ肷��
    public static void SetChoiceNum(this Player player, int choiceNum)
    {
        numToSet[ChoiceNumKey] = choiceNum;
        player.SetCustomProperties(numToSet);
        numToSet.Clear();
    }*/
}