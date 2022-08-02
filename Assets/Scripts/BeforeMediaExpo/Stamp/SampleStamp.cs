using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;//Image�^���������߂ɓ���

public class SampleStamp : MonoBehaviour
{
    [SerializeField] Image EmojiImage1 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g�Ƃ̘A�g�̂��߂ɓ���
    [SerializeField] Image EmojiImage2 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g�Ƃ̘A�g�̂��߂ɓ���
    [SerializeField] Image EmojiImage3 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g�Ƃ̘A�g�̂��߂ɓ���
    [SerializeField] Image EmojiImage4 = null;//Emoji�摜��\��������Image�I�u�W�F�N�g�Ƃ̘A�g�̂��߂ɓ���

    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;

        Debug.Log(button_ob.name);
        switch (button_ob.name)
        {
            case "1":
                EmojiImage1.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1,11));
                break;
            case "2":
                EmojiImage2.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            case "3":
                EmojiImage3.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            case "4":
                EmojiImage4.sprite = Resources.Load<Sprite>("StampImage/" + Random.Range(1, 11));
                break;
            default :
                Debug.Log("Default");
                break;
        }
        
    }
}
