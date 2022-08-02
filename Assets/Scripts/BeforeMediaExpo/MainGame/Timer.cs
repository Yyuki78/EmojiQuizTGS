using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //18å¬ÇÃéûä‘ÉQÅ[ÉWÇ1ïbÇ≤Ç∆Ç…è¡ÇµÇƒÇ¢Ç≠
    [SerializeField] GameObject Timer1;
    [SerializeField] GameObject Timer2;
    [SerializeField] GameObject Timer3;
    [SerializeField] GameObject Timer4;
    [SerializeField] GameObject Timer5;
    [SerializeField] GameObject Timer6;
    [SerializeField] GameObject Timer7;
    [SerializeField] GameObject Timer8;
    [SerializeField] GameObject Timer9;
    [SerializeField] GameObject Timer10;
    [SerializeField] GameObject Timer11;
    [SerializeField] GameObject Timer12;
    [SerializeField] GameObject Timer13;
    [SerializeField] GameObject Timer14;
    [SerializeField] GameObject Timer15;
    [SerializeField] GameObject Timer16;
    GameObject[] Timers = new GameObject[16];

    private bool once = true;
    private int Num = 16;

    // Start is called before the first frame update
    void Awake()
    {
        Timers[0] = Timer1;
        Timers[1] = Timer2;
        Timers[2] = Timer3;
        Timers[3] = Timer4;
        Timers[4] = Timer5;
        Timers[5] = Timer6;
        Timers[6] = Timer7;
        Timers[7] = Timer8;
        Timers[8] = Timer9;
        Timers[9] = Timer10;
        Timers[10] = Timer11;
        Timers[11] = Timer12;
        Timers[12] = Timer13;
        Timers[13] = Timer14;
        Timers[14] = Timer15;
        Timers[15] = Timer16;
        for(int i = 0; i < 16; i++)
        {
            Timers[i].gameObject.SetActive(false);
        }
        once = true;
        Num = 16;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetCurrentState() == GameManager.GameMode.MainGame)
        {
            if (MainGameManager2.mainmode == MainGameManager2.MainGameMode.QuestionTime)
            {
                if (once == true)
                {
                    StartCoroutine("Time");
                    for (int i = 0; i < 16; i++)
                    {
                        Timers[i].gameObject.SetActive(true);
                    }
                    once = false;
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
        for (int i = 0; i < 16; i++)
        {
            Timers[i].gameObject.SetActive(false);
        }
        once = true;
        Num = 16;
    }
}
