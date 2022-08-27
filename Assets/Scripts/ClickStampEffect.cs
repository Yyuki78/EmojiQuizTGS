using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClickStampEffect : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine("PopUp");
    }

    private IEnumerator PopUp()
    {
        yield return new WaitForSeconds(0.01f);

        for(int i = 0; i < 50; i++)
        {
            transform.position += new Vector3(0, 0.03f, 0);
            _image.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
