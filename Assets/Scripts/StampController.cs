using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class StampController : MonoBehaviourPunCallbacks
{
    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;
    private GameObject button_ob;

    [SerializeField] GameObject StampPanel;
    public static bool isActive = false; //���g�p����̂ŕ\����\���������p��

    //�ŏ��̈�̓p�l�� �Ō�̈�͑I���ł��Ȃ����邽�߂̃p�l��
    private Image[] StampImage = new Image[9];
    private bool canPress=true;

    [SerializeField]ClickStampEffect stampPrefab = default; // Prefab�̎Q��

    // �A�N�e�B�u�ȃX�^���v�̃��X�g
    private List<ClickStampEffect> activeList = new List<ClickStampEffect>();
    // ��A�N�e�B�u�ȃX�^���v�̃I�u�W�F�N�g�v�[��
    private Stack<ClickStampEffect> inactivePool = new Stack<ClickStampEffect>();

    [SerializeField] PopUpStampEffect stampPrefab2 = default;
    private List<PopUpStampEffect> activeList2 = new List<PopUpStampEffect>();
    private Stack<PopUpStampEffect> inactivePool2 = new Stack<PopUpStampEffect>();

    private PhotonView _view;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();
        StampPanel.gameObject.SetActive(false);
        StampImage = StampPanel.GetComponentsInChildren<Image>();
        for (int i = 1; i < 8; i++)
        {
            StampImage[i].color = new Color(1, 1, 1);
        }
        StampImage[8].gameObject.SetActive(false);
        if (isActive)
        {
            StampPanel.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �t���Ƀ��[�v���񂵂āAactiveList�̗v�f���r���ō폜����Ă����������[�v�����悤�ɂ���
        for (int i = activeList.Count - 1; i >= 0; i--)
        {
            var ClickStampEffect = activeList[i];
            if (!ClickStampEffect.IsActive)
            {
                Remove(ClickStampEffect);
            }
        }

        // �t���Ƀ��[�v���񂵂āAactiveList�̗v�f���r���ō폜����Ă����������[�v�����悤�ɂ���
        for (int i = activeList2.Count - 1; i >= 0; i--)
        {
            var PopUpStampEffect = activeList2[i];
            if (!PopUpStampEffect.IsActive)
            {
                Remove2(PopUpStampEffect);
            }
        }
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h�@�����̂�
    private void Active(int num)
    {
        // ��A�N�e�B�u�̃X�^���v������Ύg���񂷁A�Ȃ���ΐ�������
        var ClickStampEffect = (inactivePool.Count > 0) ? inactivePool.Pop() : Instantiate(stampPrefab, transform);

        //���W�E�摜��\������O�ɕύX����
        Image _image = ClickStampEffect.gameObject.GetComponent<Image>();
        _image.sprite = Resources.Load<Sprite>("StampImage/" + num);
        _image.DOFade(1f, 0.01f);

        ClickStampEffect.gameObject.transform.localPosition = new Vector3(-440 + (num * 110), -312.5f, 0);

        ClickStampEffect.gameObject.SetActive(true);

        ClickStampEffect.Init();
        activeList.Add(ClickStampEffect);
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h �����̂�
    private void Remove(ClickStampEffect ClickStampEffect)
    {
        activeList.Remove(ClickStampEffect);
        inactivePool.Push(ClickStampEffect);
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h�@�S���������
    [PunRPC]
    private void Active2(int num)
    {
        // ��A�N�e�B�u�̃X�^���v������Ύg���񂷁A�Ȃ���ΐ�������
        var PopUpStampEffect = (inactivePool2.Count > 0) ? inactivePool2.Pop() : Instantiate(stampPrefab2, transform);

        //���W�E�摜��\������O�ɕύX����
        Image _image = PopUpStampEffect.gameObject.GetComponent<Image>();
        _image.sprite = Resources.Load<Sprite>("StampImage/" + num);
        _image.DOFade(0.2f, 0.01f);
        PopUpStampEffect.gameObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.01f);

        int rndX, rndY;
        int isRight = Random.Range(0, 2);
        if (isRight == 0)
        {
            rndX = Random.Range(-450, -150);
        }
        else
        {
            rndX = Random.Range(150, 450);
        }

        int isTop = Random.Range(0, 2);
        if (isTop == 0)
        {
            rndY = Random.Range(-250, -100);
        }
        else
        {
            rndY = Random.Range(100, 320);
        }
        PopUpStampEffect.gameObject.transform.localPosition = new Vector3(rndX, rndY, 0);

        PopUpStampEffect.gameObject.SetActive(true);

        PopUpStampEffect.Init();
        activeList2.Add(PopUpStampEffect);
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h �S���������
    private void Remove2(PopUpStampEffect PopUpStampEffect)
    {
        activeList2.Remove(PopUpStampEffect);
        inactivePool2.Push(PopUpStampEffect);
    }

    //StampSwitchButton�Ŏg�p
    public void OnClickSwitchPanel()
    {
        if (isActive)
        {
            isActive = false;
            StartCoroutine(HidePanel());
        }
        else
        {
            isActive = true;
            StampPanel.gameObject.SetActive(true);
            StampPanel.transform.DORotate(new Vector3(0, 0, 0), 0.34f);
        }
    }

    private IEnumerator HidePanel()
    {
        StampPanel.transform.DORotate(new Vector3(90, 0, 0), 0.34f);
        yield return new WaitForSeconds(0.34f);
        StampPanel.gameObject.SetActive(false);
        yield break;
    }

    //Stamp�Ŏg�p
    public void PressStamp()
    {
        if (!canPress) return;
        canPress = false;
        for (int i = 1; i < 8; i++)
        {
            StampImage[i].color = new Color(0.8f, 0.8f, 0.8f);
        }
        StampImage[8].gameObject.SetActive(true);
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;
        int num = int.Parse(button_ob.name);
        Active(num);
        _view.RPC(nameof(Active2), RpcTarget.All, num);

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        canPress = true;
        for (int i = 1; i < 8; i++)
        {
            StampImage[i].color = new Color(1, 1, 1);
        }
        StampImage[8].gameObject.SetActive(false);
        yield break;
    }
}
