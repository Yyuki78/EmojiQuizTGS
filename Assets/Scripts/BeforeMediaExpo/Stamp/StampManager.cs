using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    [SerializeField]
    private PopUpEffect stampPrefab = default; // ProjectileのPrefabの参照

    // アクティブなスタンプのリスト
    private List<PopUpEffect> activeList = new List<PopUpEffect>();
    // 非アクティブなスタンプのオブジェクトプール
    private Stack<PopUpEffect> inactivePool = new Stack<PopUpEffect>();

    private void Update()
    {
        // 逆順にループを回して、activeListの要素が途中で削除されても正しくループが回るようにする
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

    // スタンプをアクティブ化するメソッド
    public void Fire(Vector3 origin)
    {
        // 非アクティブのスタンプがあれば使い回す、なければ生成する
        var popUpEffect = (inactivePool.Count > 0)
            ? inactivePool.Pop()
            : Instantiate(stampPrefab, transform);
        popUpEffect.Init(origin);
        activeList.Add(popUpEffect);
    }

    // スタンプを非アクティブ化するメソッド
    public void Remove(PopUpEffect popUpEffect)
    {
        activeList.Remove(popUpEffect);
        popUpEffect.Deactivate();
        inactivePool.Push(popUpEffect);
    }
}
