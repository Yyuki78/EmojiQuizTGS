using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class RoomListView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListEntry roomListEntryPrefab = default; // RoomListEntry��Prefab�̎Q��

    private ScrollRect scrollRect;
    private Dictionary<string, RoomListEntry> activeEntries = new Dictionary<string, RoomListEntry>();
    private Stack<RoomListEntry> inactiveEntries = new Stack<RoomListEntry>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // ���[�����X�g���X�V���ꂽ���ɌĂ΂��R�[���o�b�N
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            RoomListEntry entry;
            if (activeEntries.TryGetValue(info.Name, out entry))
            {
                if (!info.RemovedFromList)
                {
                    // ���X�g�v�f���X�V����
                    entry.Activate(info);
                }
            }
        }
    }
}