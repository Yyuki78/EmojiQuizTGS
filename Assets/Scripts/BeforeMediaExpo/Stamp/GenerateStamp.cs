using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStamp : MonoBehaviour
{
    [SerializeField] GameObject Parent;
    Transform pool; //�I�u�W�F�N�g��ۑ������I�u�W�F�N�g��transform

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
