using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    //12ŒÂ‚ÌŠÔƒQ[ƒW‚ğ1•b‚²‚Æ‚ÉÁ‚µ‚Ä‚¢‚­
    [SerializeField] GameObject[] Timers = new GameObject[12];

    private bool once = true;
    private int Num = 12;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            Timers[i].gameObject.SetActive(false);
        }
        once = true;
        Num = 12;
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugGameManager.Instance.GetCurrentState() == DebugGameManager.GameMode.MainGame)
        {
            if (MainGameController.mainmode == MainGameController.MainGameMode.QuestionTime)
            {
                if (once)
                {
                    StartCoroutine("Time");
                    for (int i = 0; i < 12; i++)
                    {
                        Timers[i].gameObject.SetActive(true);
                    }
                    once = false;
                }
            }else if(MainGameController.mainmode == MainGameController.MainGameMode.InformRole)
            {
                if (!once)
                {
                    Init();
                }
            }
        }
    }

    private IEnumerator Time()
    {
        while (Num > 0)
        {
            yield return new WaitForSeconds(1.0f);
            Num--;
            Timers[Num].gameObject.SetActive(false);
        }
    }

    public void Init()
    {
        for (int i = 0; i < 12; i++)
        {
            Timers[i].gameObject.SetActive(false);
        }
        once = true;
        Num = 12;
    }
}
