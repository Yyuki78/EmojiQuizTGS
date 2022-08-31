using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InRoom : MonoBehaviourPunCallbacks
{
    private PhotonView _view;

    //�Q�[���J�n����p
    private bool isStart = false;
    private bool once = true;
    private bool once2 = true;

    //�A�C�R���I����ʂ̓_�ŗp
    private bool isFlashingIcon = false;
    private bool plus1 = false;

    //���������{�^���̓_�ŗp
    private bool isFlashing = false;
    private bool plus = false;

    [SerializeField] Image StartPanel;//������ʂ��猳�ɖ߂邽�߂�Panel

    // eventSystem�^�̕ϐ���錾�@�C���X�y�N�^�[��EventSystem���A�^�b�`���Ď擾���Ă���
    [SerializeField] private EventSystem eventSystem;

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    [SerializeField] Image myIconImage; //�����̃A�C�R���摜
    [SerializeField] GameObject noSelectPanel;//�A�C�R���ȊO��I���ł��Ȃ�����p�l��
    [SerializeField] Image IconSelectImage;//�A�C�R���I����ʂ̉摜
    [SerializeField] Image ReadyButtonImage;//���������{�^���̉摜
    [SerializeField] GameObject myCheckMarkImage;//�����̏��������������`�F�b�N�}�[�N

    [SerializeField] GameObject[] otherInfo = new GameObject[4];//���̐l�����邩�ǂ���
    [SerializeField] Image[] otherIconImages = new Image[4];//���̐l�̃A�C�R��
    [SerializeField] GameObject[] otherCheckImages = new GameObject[4];//���̐l�̃`�F�b�N�}�[�N

    [SerializeField] GameObject[] alreadySelectIconImages = new GameObject[6];//���ɑI�����ꂽ�A�C�R�����B���p

    //���n
    [SerializeField] GameObject AudioManager;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        _view = GetComponent<PhotonView>();
        _audio = AudioManager.GetComponent<AudioManager>();
        StartPanel.gameObject.SetActive(true);
        noSelectPanel.SetActive(true);
        myCheckMarkImage.SetActive(false);
        for(int i = 0; i < 4; i++)
        {
            otherInfo[i].SetActive(false);
            otherCheckImages[i].SetActive(false);
        }
        for(int i = 0; i < 6; i++)
        {
            alreadySelectIconImages[i].SetActive(false);
        }
        isFlashingIcon = true;

        PhotonNetwork.LocalPlayer.SetScore(10);

        StartCoroutine(StartEffect());
        //StartCoroutine(ShareIconInfo());
        StartCoroutine(SharePlayerInfo());
    }

    // Update is called once per frame
    void Update()
    {
        //�A�C�R���I���p�l���̓_��
        if (isFlashingIcon)
        {
            if (IconSelectImage.color.a >= 0.9f)
            {
                plus1 = false;
            }
            if (IconSelectImage.color.a <= 0.5f)
            {
                plus1 = true;
            }
            if (!plus1)
            {
                IconSelectImage.color -= new Color(0, 0, 0, 0.0075f);
            }
            else
            {
                IconSelectImage.color += new Color(0, 0, 0, 0.0075f);
            }
        }

        //���������{�^���̓_��
        if (isFlashing)
        {
            if (ReadyButtonImage.color.a >= 1)
            {
                plus = false;
            }
            if (ReadyButtonImage.color.a <= 0.75f)
            {
                plus = true;
            }
            if (!plus)
            {
                ReadyButtonImage.color -= new Color(0, 0, 0, 0.0075f);
            }
            else
            {
                ReadyButtonImage.color += new Color(0, 0, 0, 0.0075f);
            }
        }


        //�Q�[���J�n�̔���
        if (PhotonNetwork.IsMasterClient)
        {

            //�S�v���C���[�擾
            Player[] Players = PhotonNetwork.PlayerList;

            //���ɐl�����Ȃ��Ȃ�return
            if (Players.Length <= 1) return;

            //�����������ǂ�����l�������肵������
            for (int i = 0; i < Players.Length; i++)
            {
                bool hantei = Players[i].GetReady();
                if (!hantei)
                {
                    return;
                }
            }

            //�S�������������Ȃ�
            _view.RPC(nameof(StartMainGame), RpcTarget.All);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        if (isStart && once)
        {
            StartCoroutine(CountDown());
            once = false;
        }
    }

    [PunRPC]
    private void StartMainGame()
    {
        isStart = true;
    }

    private IEnumerator CountDown()
    {
        //�J�E���g�_�E��
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");

        var players = PhotonNetwork.PlayerList;
        yield return new WaitForSeconds(0.01f);

        //���ꂼ��̔ԍ���ݒ�
        int i = 0;
        foreach (var player in players)
        {
            if (PhotonNetwork.LocalPlayer == player)
            {
                PhotonNetwork.LocalPlayer.UpdatePlayerNum(i);
            }
            i++;
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("�Q�[���X�^�[�g�I");
        DebugGameManager.Instance.SetCurrentState(DebugGameManager.GameMode.MainGame);
        yield break;
    }


    public void OnClickIcon()
    {
        _audio.SE1();

        //���������{�^����������悤�ɂ���
        noSelectPanel.SetActive(false);

        // �A�C�R����ݒ肷��
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;
        Debug.Log("�I�����ꂽ�̂�" + button_ob.name);
        int x = int.Parse(button_ob.name);
        Debug.Log("�摜�ԍ���" + x);
        PhotonNetwork.LocalPlayer.SetScore(x);

        myIconImage.sprite = Resources.Load<Sprite>("IconImage/" + x);

        if (once2)
        {
            once2 = false;
            IconSelectImage.color = new Color(1, 1, 1, 0.8f);
            isFlashingIcon = false;
            isFlashing = true;
        }
    }

    public void OnClickReadyButton()
    {
        Debug.Log("���������I");
        _audio.SE1();

        // ��������
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LocalPlayer.SetReady(true);
        }
        // ���������}�[�N���o��
        isFlashing = false;
        ReadyButtonImage.color = new Color(1, 1, 1, 1);

        myCheckMarkImage.SetActive(true);
    }

    //���ɑI�����ꂽ�A�C�R����I��s�ɂ���
    private IEnumerator ShareIconInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            var players = PhotonNetwork.PlayerList;

            for (int i = 1; i < 7; i++)
            {
                foreach (var player in players)
                {
                    if (player.GetScore() == i)
                    {
                        alreadySelectIconImages[i].SetActive(true);
                    }
                    else
                    {
                        alreadySelectIconImages[i].SetActive(false);
                    }
                }
            }
        }
    }

    //�����ȊO�̐l�̃A�C�R���A�`�F�b�N�}�[�N�̋��L
    private IEnumerator SharePlayerInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            //���̑S�v���C���[�擾
            Player[] otherPlayers = PhotonNetwork.PlayerListOthers;

            for (int i = 0; i < otherPlayers.Length; i++)
            {
                otherInfo[i].SetActive(true);
            }

            switch (otherPlayers.Length)
            {
                case 0:
                    Debug.Log("��l");
                    break;
                case 1:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    otherInfo[1].SetActive(false);
                    otherInfo[2].SetActive(false);
                    otherInfo[3].SetActive(false);
                    break;
                case 2:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    otherInfo[2].SetActive(false);
                    otherInfo[3].SetActive(false);
                    break;
                case 3:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    otherIconImages[2].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[2].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    if (otherPlayers[2].GetReady())
                    {
                        otherCheckImages[2].SetActive(true);
                    }
                    otherInfo[3].SetActive(false);
                    break;
                case 4:
                    otherIconImages[0].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[0].GetScore());
                    otherIconImages[1].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[1].GetScore());
                    otherIconImages[2].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[2].GetScore());
                    otherIconImages[3].sprite = Resources.Load<Sprite>("IconImage/" + otherPlayers[3].GetScore());
                    if (otherPlayers[0].GetReady())
                    {
                        otherCheckImages[0].SetActive(true);
                    }
                    if (otherPlayers[1].GetReady())
                    {
                        otherCheckImages[1].SetActive(true);
                    }
                    if (otherPlayers[2].GetReady())
                    {
                        otherCheckImages[2].SetActive(true);
                    }
                    if (otherPlayers[3].GetReady())
                    {
                        otherCheckImages[3].SetActive(true);
                    }
                    break;
                default:
                    Debug.Log("�����Ă���l�������������ł�");
                    break;
            }
        }
    }

    // ���̃v���C���[���ގ�������
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        
        var players = PhotonNetwork.PlayerList;
        int i = 0;
        foreach (var player in players)
        {
            if (otherPlayer == player)
            {
                otherInfo[i].SetActive(false);
                otherIconImages[i].sprite = Resources.Load<Sprite>("IconImage/10");
                otherCheckImages[i].SetActive(false);
            }
            i++;
        }
    }

    private IEnumerator StartEffect()
    {
        for(int i = 0; i < 50; i++)
        {
            StartPanel.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        StartPanel.gameObject.SetActive(false);
        yield break;
    }
}
