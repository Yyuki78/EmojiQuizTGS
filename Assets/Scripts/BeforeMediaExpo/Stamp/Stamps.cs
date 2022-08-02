using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamps : MonoBehaviour
{
    //
    [SerializeField] GameObject Parent;
    Transform pool; //オブジェクトを保存する空オブジェクトのtransform
    PopUpEffect _popUpEffect;

    //生成するスタンプ
    [SerializeField] GameObject popUpStamp = null;

    void Start()
    {
        pool = Parent.transform;
    }

    public void OnClickStamp()
    {
        //弾生成関数を呼び出し
        InstStamp(transform.position, transform.rotation);
    }

    void InstStamp(Vector3 pos, Quaternion qua)
    {
        foreach (Transform t in pool)
        {
            //オブジェが非アクティブなら使い回し
            if (!t.gameObject.activeSelf)
            {
                t.SetPositionAndRotation(pos, qua);
                _popUpEffect = t.gameObject.GetComponent<PopUpEffect>();
                t.gameObject.SetActive(true);//位置と回転を設定後、アクティブにする
                //_popUpEffect.Init();
            }
        }
        //非アクティブなオブジェクトがないなら生成
        Instantiate(popUpStamp, pos, qua, pool);//生成と同時にpoolを親に設定
    }
}
