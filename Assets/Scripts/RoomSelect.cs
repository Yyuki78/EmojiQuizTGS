using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelect : MonoBehaviour
{
    [SerializeField] GameObject[] PanelMassObj = new GameObject[2];
    [SerializeField] GameObject[] DoorMassObj = new GameObject[2];

    [SerializeField] RoomMatchingSystem _matching;

    [SerializeField] Image BlackOutPanel;

    // Start is called before the first frame update
    void Start()
    {
        BlackOutPanel.gameObject.SetActive(false);
    }

    public void OnClickRoom1()
    {
        StartCoroutine(GoRoomEffect(1));
    }

    public void OnClickRoom2()
    {
        StartCoroutine(GoRoomEffect(2));
    }

    private IEnumerator GoRoomEffect(int num)
    {
        //ドアが開く＋画面がそこに寄る(pivotを変更してScaleを拡大する)
        this.gameObject.transform.parent = PanelMassObj[num - 1].transform;
        BlackOutPanel.gameObject.SetActive(true);
        DoorMassObj[num - 1].transform.localEulerAngles = new Vector3(0, -30, 0);

        for (int i = 0; i < 100; i++)
        {
            DoorMassObj[num - 1].transform.localEulerAngles += new Vector3(0, -1, 0);
            PanelMassObj[num - 1].transform.localScale += new Vector3(0.04f, 0.04f, 0);
            BlackOutPanel.color += new Color(0, 0, 0, 0.0075f);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 10; i++)
        {
            BlackOutPanel.color += new Color(0, 0, 0, 0.025f);
            yield return new WaitForSeconds(0.015f);
        }
        BlackOutPanel.color = new Color(0, 0, 0, 1);

        //部屋に入る
        switch (num)
        {
            case 1:
                _matching.JoinOrCreateRoom1();
                break;
            case 2:
                _matching.JoinOrCreateRoom2();
                break;
            default:
                Debug.Log(num + "がおかしいです");
                break;
        }

        yield break;
    }
}
