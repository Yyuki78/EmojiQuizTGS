using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkOperate : MonoBehaviour
{
    public static NetworkOperate Operate;

    byte MainPlayer;
    byte[] ChoiceEmoji = new byte[5];
    byte CorrectAnswer;
    int StandbyTime;
    byte[] PlayersAnswer = new byte[5];

    public byte getMainPlayer()
    {
        return CorrectAnswer;
    }
    public byte[] getChoiceEmoji()
    {
        return ChoiceEmoji;
    }

    public byte getCorrectAnswer()
    {
        return CorrectAnswer;
    }

    public int getStandbyTime()
    {
        Debug.Log("InGetStandByTime");
        Debug.Log("StandByTime = " + StandbyTime);
        Debug.Log("OutGetStandByTime");
        return StandbyTime;
    }

    public byte[] getPlayerAnswer()
    {
        return PlayersAnswer;
    }

    [PunRPC]
    public void SelectPlayer(byte MP)
    {
        MainPlayer = MP;
    }

    [PunRPC]
    public void OperateQuestion(byte[] CE, byte CA)
    {
        ChoiceEmoji = CE;
        CorrectAnswer = CA;
    }

    [PunRPC]
    public void SendServerTime(int ST)
    {
        StandbyTime = ST;
    }

    [PunRPC]
    public void SendMyAnswer(byte MA, byte PI)
    {
        PlayersAnswer[PI] = MA;
    }

    [PunRPC]
    public void SendOurAnswers(byte[] OA)
    {
        PlayersAnswer = OA;
    }
}
