using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PrintTextTest : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject TextObj;
    Text text;
    string makingtext;
    short modeReceiver;
    int countReceiver;
    
    // Start is called before the first frame update
    void Start()
    {
        text = TextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        makingtext = "1 " + MainGameManager.mainmode + " " + MainGameManager.playcount + " " + MainGameManager.modetime + "\n";
        makingtext += "2 " + (MainGameManager.MainGameMode)modeReceiver + " " + countReceiver + "\n";
        text.text = makingtext;
    }

}
