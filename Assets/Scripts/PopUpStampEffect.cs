using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpStampEffect : MonoBehaviour
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

        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        _image.DOFade(0.9f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        yield return new WaitForSeconds(1.5f);

        transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
        _image.DOFade(0.2f, 1);

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
