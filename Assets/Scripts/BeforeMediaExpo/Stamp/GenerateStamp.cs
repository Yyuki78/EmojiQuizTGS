using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStamp : MonoBehaviour
{
    [SerializeField] GameObject Parent;
    Transform pool; //オブジェクトを保存する空オブジェクトのtransform

    private StampManager StampManager;

    void Start()
    {
        pool = Parent.transform;
        StampManager = Parent.GetComponent<StampManager>();
    }

    public void StampGenerate()
    {
        StampManager.Fire(transform.position);
    }
}
