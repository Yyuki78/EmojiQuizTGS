using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamps : MonoBehaviour
{
    //
    [SerializeField] GameObject Parent;
    Transform pool; //�I�u�W�F�N�g��ۑ������I�u�W�F�N�g��transform
    PopUpEffect _popUpEffect;

    //��������X�^���v
    [SerializeField] GameObject popUpStamp = null;

    void Start()
    {
        pool = Parent.transform;
    }

    public void OnClickStamp()
    {
        //�e�����֐����Ăяo��
        InstStamp(transform.position, transform.rotation);
    }

    void InstStamp(Vector3 pos, Quaternion qua)
    {
        foreach (Transform t in pool)
        {
            //�I�u�W�F����A�N�e�B�u�Ȃ�g����
            if (!t.gameObject.activeSelf)
            {
                t.SetPositionAndRotation(pos, qua);
                _popUpEffect = t.gameObject.GetComponent<PopUpEffect>();
                t.gameObject.SetActive(true);//�ʒu�Ɖ�]��ݒ��A�A�N�e�B�u�ɂ���
                //_popUpEffect.Init();
            }
        }
        //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��Ȃ琶��
        Instantiate(popUpStamp, pos, qua, pool);//�����Ɠ�����pool��e�ɐݒ�
    }
}
