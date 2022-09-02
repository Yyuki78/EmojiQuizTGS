using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InductionImageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            for(int i = 0; i < 50; i++)
            {
                transform.localPosition -= new Vector3(0, 0.2f, 0);
                yield return new WaitForSeconds(0.015f);
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 100; i++)
            {
                transform.localPosition += new Vector3(0, 0.2f, 0);
                yield return new WaitForSeconds(0.015f);
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 50; i++)
            {
                transform.localPosition -= new Vector3(0, 0.2f, 0);
                yield return new WaitForSeconds(0.015f);
            }
        }
    }
}
