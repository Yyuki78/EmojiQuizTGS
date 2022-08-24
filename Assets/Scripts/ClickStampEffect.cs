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

        transform.DOMoveY(150, 1.5f);
        _image.DOFade( 0f,1.5f);

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
