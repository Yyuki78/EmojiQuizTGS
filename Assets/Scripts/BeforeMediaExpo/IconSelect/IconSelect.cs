using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;//Image�^���������߂ɓ���
public class IconSelect : MonoBehaviour
{
    [SerializeField] Image EmojiImage = null;//Emoji�摜��\��������Image�I�u�W�F�N�g�Ƃ̘A�g�̂��߂ɓ���

    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    //GameObject�^�̕ϐ���錾
    [SerializeField] GameObject OKbutton;

    //�{�^���ɂ��̊֐������蓖�ĂĎg�p
    public void ChangeButtonTextName()
    {
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;

        Debug.Log(button_ob.name);
        EmojiImage.sprite = Resources.Load<Sprite>("Image/"+ button_ob.name);
        OKbutton.SetActive(true);
        //Player�̖��O�ƃX�R�A���G�����ԍ��ɂ���
        PhotonNetwork.NickName = button_ob.name;
        PhotonNetwork.LocalPlayer.SetScore(int.Parse(button_ob.name));
    }
    /*
    //�u1�v�{�^���������ꂽ���̓���BImage�I�u�W�F�N�g��1�̉摜��\��������
    public void ClickButton1()
    {
        Debug.Log(this.gameObject.name);
        //Assets������Resources�t�H���_�����ɂ���Image/1�ƌ����p�X�ŕ\�����t�@�C�����A�ϐ�EmojiImage�ƌ��ѕt����ꂽ�I�u�W�F�N�g�ɕ\�������鏈���B.png�Ȃǂ̊g���q�͕s�v
        EmojiImage.sprite = Resources.Load<Sprite>("Image/1");
    }
    //�u2�v�{�^���������ꂽ���̓���BImage�I�u�W�F�N�g��2�̉摜��\��������
    public void ClickButton2()
    {
        //Assets������Resources�t�H���_�����ɂ���Image/pipo-enemy011�ƌ����p�X�ŕ\�����t�@�C�����A�ϐ�EnemyImage�ƌ��ѕt����ꂽ�I�u�W�F�N�g�ɕ\�������鏈���B.png�Ȃǂ̊g���q�͕s�v
        EmojiImage.sprite = Resources.Load<Sprite>("Image/2");
    }
    //�u3�v�{�^���������ꂽ���̓���BImage�I�u�W�F�N�g�Ƀh���S���̉摜��\��������
    public void ClickButton3()
    {
        //Assets������Resources�t�H���_�����ɂ���Image/pipo-enemy021�ƌ����p�X�ŕ\�����t�@�C�����A�ϐ�EnemyImage�ƌ��ѕt����ꂽ�I�u�W�F�N�g�ɕ\�������鏈���B.png�Ȃǂ̊g���q�͕s�v
        EmojiImage.sprite = Resources.Load<Sprite>("Image/3");
    }
    //�u4�v�{�^���������ꂽ���̓���BImage�I�u�W�F�N�g�Ƀh���S���̉摜��\��������
    public void ClickButton4()
    {
        //Assets������Resources�t�H���_�����ɂ���Image/pipo-enemy021�ƌ����p�X�ŕ\�����t�@�C�����A�ϐ�EnemyImage�ƌ��ѕt����ꂽ�I�u�W�F�N�g�ɕ\�������鏈���B.png�Ȃǂ̊g���q�͕s�v
        EmojiImage.sprite = Resources.Load<Sprite>("Image/4");
    }
    */
}