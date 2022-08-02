using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    //Rigidbody�^��ϐ�rigid�Ő錾���܂��B
    private Rigidbody rigid;
    //Vector3�^��ϐ�pos�Ő錾���܂��B
    private Vector3 pos = new Vector3(1450+512, 374, 0);
    //float�^��ϐ�speed�Ő錾
    private float speed = 20f;

    private bool once = false;

    void Start()
    {

        //GetComponent��Rigidbody���擾���ĕϐ�rigid�ŎQ�Ƃ��܂��B
        rigid = GetComponent<Rigidbody>();

        rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
        //1.5�b��ɏI���
        //Invoke(nameof(VanishMethod), 1.5f);
        //StartCoroutine("Change");
    }

    private void Update()
    {
        if (once == true)
        {
            once = false;
            StartCoroutine("Change");
        }
    }

    public IEnumerator Change()
    {
        pos = new Vector3(1450 + 512, 374, 0);
        rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));

        // �w��b�ԑ҂�
        //yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 150; i++)
        {

            //Y���W��speed�̒l�𑫂��ꂽ�ʒu�ɁB
            pos.x = pos.x - speed;
            //�ϐ�rigid�̓������W�ʒu�̎擾�B
            rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));

            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2.0f);
        pos = new Vector3(1450 + 512, 374, 0);
        rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
        //this.gameObject.SetActive(false);
    }

    public void Init()
    {
        once = true;
    }
}
