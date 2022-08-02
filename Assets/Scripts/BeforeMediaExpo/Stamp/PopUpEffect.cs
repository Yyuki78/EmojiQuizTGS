using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpEffect : MonoBehaviour
{
    //Rigidbody型を変数rigidで宣言します。
    private Rigidbody rigid;
    //Vector3型を変数posで宣言します。
    private Vector3 pos;
    //float型を変数speedで宣言して最初の値は5とします。
    private float speed = 1f;

    public bool IsActive => gameObject.activeSelf;

    Image mesh;

    private void Awake()
    {
        pos = transform.position;
        mesh = GetComponent<Image>();
        mesh.color = mesh.color - new Color32(0, 0, 0, 20);
        //GetComponentでRigidbodyを取得して変数rigidで参照します。
        rigid = GetComponent<Rigidbody>();

        //StartCoroutine("PopUp");

    }

    public void Init(Vector3 orizin)
    {
        pos = orizin;
        rigid.position = pos;
        gameObject.SetActive(true);
        StartCoroutine("PopUp");
    }
    /*
    private void FixedUpdate()
    {
        //Y座標はspeedの値を足された位置に。
        pos.y = pos.y + speed;
        //変数rigidの動く座標位置の取得。
        rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
        
        //徐々に透明化
        mesh.color = mesh.color - new Color32(0, 0, 0, 10);
    }

    private void VanishMethod()
    {
        this.gameObject.SetActive(false);
    }*/

    private IEnumerator PopUp()
    {
        rigid.position = pos;
        // 指定秒間待つ
        yield return new WaitForSeconds(0.1f);
        //rigid.MovePosition(transform.position);
        mesh.color = new Color32(255, 255, 255, 235);

        for (int i = 0; i < 100; i++)
        {
            //徐々に透明化
            mesh.color = mesh.color - new Color32(0, 0, 0, 2);

            //Y座標はspeedの値を足された位置に。
            pos.y = pos.y + speed;
            //変数rigidの動く座標位置の取得。
            //rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
            rigid.position = pos;

            yield return new WaitForSeconds(0.005f);
        }
        pos.y = 1500;
        this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
