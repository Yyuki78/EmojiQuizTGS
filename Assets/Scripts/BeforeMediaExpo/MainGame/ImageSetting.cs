using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetting : MonoBehaviour
{
    //スタンプが出る場所
    [SerializeField] private Image StampImage1;
    [SerializeField] private Image StampImage2;
    [SerializeField] private Image StampImage3;
    [SerializeField] private Image StampImage4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //位置と場所の数値を受け取って画像を出すプログラム
    void show(int pos,int num)
    {
        switch (pos)
        {
            case 1:
                StampImage1.sprite = Resources.Load<Sprite>("StampImage/" + num);
                break;
            case 2:
                StampImage2.sprite = Resources.Load<Sprite>("StampImage/" + num);
                break;
            case 3:
                StampImage3.sprite = Resources.Load<Sprite>("StampImage/" + num);
                break;
            case 4:
                StampImage4.sprite = Resources.Load<Sprite>("StampImage/" + num);
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
