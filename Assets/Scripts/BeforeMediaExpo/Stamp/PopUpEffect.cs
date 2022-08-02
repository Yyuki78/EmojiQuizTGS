using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpEffect : MonoBehaviour
{
    //Rigidbody�^��ϐ�rigid�Ő錾���܂��B
    private Rigidbody rigid;
    //Vector3�^��ϐ�pos�Ő錾���܂��B
    private Vector3 pos;
    //float�^��ϐ�speed�Ő錾���čŏ��̒l��5�Ƃ��܂��B
    private float speed = 1f;

    public bool IsActive => gameObject.activeSelf;

    Image mesh;

    private void Awake()
    {
        pos = transform.position;
        mesh = GetComponent<Image>();
        mesh.color = mesh.color - new Color32(0, 0, 0, 20);
        //GetComponent��Rigidbody���擾���ĕϐ�rigid�ŎQ�Ƃ��܂��B
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
        //Y���W��speed�̒l�𑫂��ꂽ�ʒu�ɁB
        pos.y = pos.y + speed;
        //�ϐ�rigid�̓������W�ʒu�̎擾�B
        rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
        
        //���X�ɓ�����
        mesh.color = mesh.color - new Color32(0, 0, 0, 10);
    }

    private void VanishMethod()
    {
        this.gameObject.SetActive(false);
    }*/

    private IEnumerator PopUp()
    {
        rigid.position = pos;
        // �w��b�ԑ҂�
        yield return new WaitForSeconds(0.1f);
        //rigid.MovePosition(transform.position);
        mesh.color = new Color32(255, 255, 255, 235);

        for (int i = 0; i < 100; i++)
        {
            //���X�ɓ�����
            mesh.color = mesh.color - new Color32(0, 0, 0, 2);

            //Y���W��speed�̒l�𑫂��ꂽ�ʒu�ɁB
            pos.y = pos.y + speed;
            //�ϐ�rigid�̓������W�ʒu�̎擾�B
            //rigid.MovePosition(new Vector3(pos.x, pos.y, pos.z));
            rigid.position = pos;

            yield return new WaitForSeconds(0.005f);
        }
        pos.y = 1500;
        this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        //��ʊO�ɍs�������A�N�e�B�u�ɂ���
        gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
