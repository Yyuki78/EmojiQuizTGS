using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    [SerializeField]
    private PopUpEffect stampPrefab = default; // Projectile��Prefab�̎Q��

    // �A�N�e�B�u�ȃX�^���v�̃��X�g
    private List<PopUpEffect> activeList = new List<PopUpEffect>();
    // ��A�N�e�B�u�ȃX�^���v�̃I�u�W�F�N�g�v�[��
    private Stack<PopUpEffect> inactivePool = new Stack<PopUpEffect>();

    private void Update()
    {
        // �t���Ƀ��[�v���񂵂āAactiveList�̗v�f���r���ō폜����Ă����������[�v�����悤�ɂ���
        for (int i = activeList.Count - 1; i >= 0; i--)
        {
            var popUpEffect = activeList[i];
            if (popUpEffect.IsActive)
            {
                //popUpEffect.OnUpdate();
            }
            else
            {
                Remove(popUpEffect);
            }
        }
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h
    public void Fire(Vector3 origin)
    {
        // ��A�N�e�B�u�̃X�^���v������Ύg���񂷁A�Ȃ���ΐ�������
        var popUpEffect = (inactivePool.Count > 0)
            ? inactivePool.Pop()
            : Instantiate(stampPrefab, transform);
        popUpEffect.Init(origin);
        activeList.Add(popUpEffect);
    }

    // �X�^���v���A�N�e�B�u�����郁�\�b�h
    public void Remove(PopUpEffect popUpEffect)
    {
        activeList.Remove(popUpEffect);
        popUpEffect.Deactivate();
        inactivePool.Push(popUpEffect);
    }
}
